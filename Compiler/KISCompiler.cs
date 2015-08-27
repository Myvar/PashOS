using Compiler.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class KISCompiler
    {
        public string PashAsm { get; set; }

        private Dictionary<string, string> VarTable = new Dictionary<string, string>();
        private Dictionary<string, string> FnTable = new Dictionary<string, string>();
        private int VarCount = 0;
        private int FnCount = 0;

        public KISCompiler()
        {

        }

        public void Compile(string src)
        {
            var a = src;
            var parsed = KISParser.Parse(a);
           
            if(ErrorStack.Errors.Count == 0)
            {
                var ast = Astifi.AstIt(parsed);
                ast.Add(new FnStmt() { CallName = "Program Exit Point" });
                VarTable = new Dictionary<string, string>();
                VarCount = 0;
                Resolve(ast);
            }
            else
            {
                foreach (var i in ErrorStack.Errors)
                {
                    Console.WriteLine(i);
                    Console.ReadLine();
                }
            }

        }



        public void Resolve(List<Iast> src)
        {
            //meta calcs
            foreach (var i in src)
            {
                if (i is SetStmt)
                {
                    var x = i as SetStmt;
                    if (!VarTable.ContainsKey(x.VarName))
                    {
                        VarTable.Add(x.VarName, VarCount.ToString());
                        VarCount++;
                    }

                }
                if (i is FnStmt)
                {
                    var x = i as FnStmt;
                    if (!FnTable.ContainsKey(x.CallName))
                    {
                        FnTable.Add(x.CallName, FnCount.ToString());
                        FnCount++;
                    }

                }
             
            }
            //actions calcs
            foreach (var i in src)
            {
                if (i is SetStmt)
                {
                    var x = i as SetStmt;
                    AddAsm("[" + VarTable[x.VarName] + "] = " + HandleValue(x.VarValue));
                }
                if (i is CallStmt)
                {
                    var x = i as CallStmt;
                    var n = VarCount;
                    if (FnTable.ContainsKey(x.Path))
                    {
                        AddAsm("[" + n + "] = {" + FnTable[x.Path] + "}");
                    }
                    else
                    {
                        AddAsm("[" + n + "] = " + HandleValue(x.Path));
                    }
                    int index = 0;
                    foreach (var z in x.Perams)
                    {
                        VarCount++;
                        AddAsm("[" + VarCount + "] = " + HandleValue(z));
                        AddAsm("MP." + index + " [" + n + "]" + " [" + VarCount + "]");
                        index++;
                    }
                    AddAsm("CALL " + "[" + n + "]");

                    VarCount++;


                }
                if (i is FnStmt)
                {
                    var x = i as FnStmt;
                    AddAsm("RET");
                    AddAsm("{" + FnTable[x.CallName] + "} (" + x.Perams.Count + ")");
                }
            }
        }

        private string HandleValue(string r)
        {
            foreach(var i in VarTable)
            {
                r = r.Replace(i.Key, "[" + i.Value + "]");
            }
            return r;
        }

        private void AddAsm(string s)
        {
            PashAsm += s + "\n";
        }
    }
}
