using PashOS.Interpreter.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PashOS.Interpreter.AST;
using PashOS.Std;

namespace PashOS.Interpreter
{
    public class Engine
    {
        public Engine()
        {

        }

        //public
        public List<MetaPackage> Meta { get; set; } = new List<MetaPackage>();
        public List<Iast> Ast = new List<Iast>();
        public bool ProgramHasEnded = false;

        //private
        private Parser _p = new Parser();
        private object[] VarArray = new object[10000];
        private MathParser mp = new MathParser();
        private List<int> Vars = new List<int>();
        private Lib.Dictionary<int, int> MethodTable = new Lib.Dictionary<int, int>();
        private List<int> ReturnStack = new List<int>();
        private int ProgramCounter = 0;
        
        public void Step()
        {
            try {
                Reolve(Ast, ProgramCounter);
                ProgramCounter++;
            }
            catch(Exception ee)
            {
                ProgramHasEnded = true;
            }
        }

        public void Load(string src, bool DoNotAutostep = true)
        {
            var ast = _p.Parse(src);
            Ast = ast;
            ProgramCounter = 0;
            ProgramHasEnded = false;
            MethodTable = new Lib.Dictionary<int, int>();
            
            for (int i = 0; i < ast.Count; i++)
            {
                if (ast[i] is MethodStmt)
                {
                    var x = ast[i] as MethodStmt;
                    MethodTable.Add(x.ID, i);
                }
            }

            if (!DoNotAutostep)
            {
                while(!ProgramHasEnded)
                {
                    Step();
                }
            }
        }

        public void Reolve(List<Iast> aAst, int index)
        {
            var i = aAst[index];

            if (i is SetStmt)
            {
                var x = i as SetStmt;

                VarArray[x.ID] = ResolveRawValue(x.Value);
                Vars.Add(x.ID);
            }
            if (i is MPStmt)
            {
                var x = i as MPStmt;
                var z = VarArray[x.MethodID];
                if (z is Method)
                {
                    if (((VarArray[x.MethodID] as Method).Perms.Count) <= x.ParameterID)
                    {
                        (VarArray[x.MethodID] as Method).Perms.Add(null);
                    }
                    (VarArray[x.MethodID] as Method).Perms[x.ParameterID] = x.Value;
                }

            }
            if (i is CallStmt)
            {
                var x = i as CallStmt;
                if ((VarArray[x.ID] as Method).ID != -1)
                {
                    ReturnStack.Add(ProgramCounter);
                }
                Call(VarArray[x.ID] as Method);
            }
            if(i is LogicStmt)
            {
                var x = i as LogicStmt;
                if(x.Opcode == "==")
                {
                    var s = ResolveRawValue(x.Left);
                    var g = ResolveRawValue(x.Right);
                    if (s is string)
                    {
                        if ((string)s == (string)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                    if (s is int)
                    {
                        if ((int)s == (int)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }

                }
                if (x.Opcode == "!=")
                {
                    var s = ResolveRawValue(x.Left);
                    var g = ResolveRawValue(x.Right);
                    if (s is string)
                    {
                        if ((string)s != (string)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                    if (s is int)
                    {
                        if ((int)s != (int)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                }
                if (x.Opcode == ">")
                {
                    var s = ResolveRawValue(x.Left);
                    var g = ResolveRawValue(x.Right);
                    if (s is string)
                    {
                        if (s.ToString().Length > g.ToString().Length)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                    if (s is int)
                    {
                        if ((int)s > (int)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                }
                if (x.Opcode == "<")
                {
                    var s = ResolveRawValue(x.Left);
                    var g = ResolveRawValue(x.Right);
                    if (s is string)
                    {
                        if (s.ToString().Length < g.ToString().Length)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                    if (s is int)
                    {
                        if ((int)s < (int)g)
                        {
                            ReturnStack.Add(ProgramCounter);
                            ProgramCounter = MethodTable[(VarArray[x.ID] as Method).ID];
                        }
                    }
                }

            }
            if (i is RetStmt)
            {
                
                ProgramCounter = ReturnStack.Last();
                ReturnStack.RemoveAt(ReturnStack.Count - 1);
            }
        }

        public object Call(Method m)
        {

            if(m.ID != -1)
            {
                ProgramCounter = MethodTable[m.ID];

                return null;
            }

            List<string> path = m.Name.Split(':').First().Split('.').ToList();
            string lib = path.Last();
            path.Remove(path.Last());
            string BasePath = path.First();
            path.Remove(path.First());
            Package package;
            if (path.Count > 0)
                package = Package.getBasePackage(BasePath).getPackageAt(path);
            else package = Package.getBasePackage(BasePath);
            List<object> args = new List<object>();
            for (int i = 0; i < m.Perms.Count; i++)
            {
                args.Add(ResolveRawValue(m.Perms[i]));
            }
            var s2 = m.Name.Split(':').Last();
            Library Lib = package.getLibrary(lib);
            var d = Lib.Functions.Get(s2);
            return d.DynamicInvoke(args.ToArray());
        }

        public object ResolveRawValue(string s)
        {
            if(s.StartsWith("\"") && s.EndsWith("\""))
            {
                return s.Trim('"');
            }
            else
            {
                if (s.Contains("/") || (s.Contains("*") && (!s.StartsWith("*") && !s.EndsWith("*"))) || s.Contains("-") || s.Contains("+") || s.Contains("%"))
                {
                    string d = s;
                    for (int i = 0; i < Vars.Count; i++)
                    {
                        d = d.Replace("[" + i + "]", VarArray[i].ToString());
                    }

                    return mp.Parse(d);
                }
                else
                {
                     if(s.StartsWith("[") && s.EndsWith("]"))
                {
                    var z = VarArray[int.Parse(s.TrimStart('[').TrimEnd(']'))];
                    if(z is Method)
                    {
                        return Call(z as Method);
                    }
                    return z;
                }
                    else
                    {
                        if (s.Contains("::"))
                        {
                            //method
                            return new Method() { Name = s };
                        }
                        else
                        {
                            if (s.StartsWith("{") && s.EndsWith("}"))
                            {
                                var x = int.Parse(s.TrimStart('{').TrimEnd('}'));
                                return new Method() { ID = x };
                            }
                            else
                            {
                                if (s.StartsWith("*") && s.EndsWith("*"))
                                {
                                    List<object> array = new List<object>();
                                    var x = s.Remove(0, 1).Remove(s.Length - 2, 1);
                                    foreach(var i in x.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        array.Add(ResolveRawValue(i));
                                    }
                                    return array.ToArray();
                                }
                                else
                                {
                                    try
                                    {

                                        return decimal.Parse(s);
                                    }
                                    catch (Exception ee)
                                    {
                                        return s;
                                    }

                                }
                            }

                            

                        }
                    }

                }
            }

            return null;
        }


        public void ParseHeader(string src)
        {
          //  Meta = MetaInfo.Parse(src);
        }
    }


  
}
