using PashOS.Interpreter.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PashOS.Interpreter.AST;

namespace PashOS.Interpreter
{
    public class Engine
    {
        public Engine(string Source)
        {
            var ast = _p.Parse(Source);
            Ast = ast;
        }

        //public
        public List<MetaPackage> Meta { get; set; } = new List<MetaPackage>();
        public List<Iast> Ast = new List<Iast>();

        //private
        private Parser _p = new Parser();
        private object[] VarArray = new object[10000];
        private MathParser mp = new MathParser();
        private List<int> Vars = new List<int>();

        public void Reolve(List<Iast> aAst, int index)
        {
            var i = aAst[index];

            if(i is SetStmt)
            {
                var x = i as SetStmt;
                
                VarArray[x.ID] = ResolveRawValue(x.Value);
                Vars.Add(x.ID);
            }
        }

        public object ResolveRawValue(string s)
        {
            if(s.StartsWith("\"") && s.EndsWith("\""))
            {
                return s.Trim('"');
            }
            else
            {

                if (s.Contains("/") || s.Contains("*") || s.Contains("-") || s.Contains("+") || s.Contains("%"))
                {
                    int f;
                    if (s.StartsWith("["))
                    {
                        Console.WriteLine(int.Parse(s.TrimStart('[').Split(']')[0]));
                        //f = VarArray[int.Parse(s.TrimStart('[').Split(']')[0])];
                    }

                    string d = s;
                    for (int i = 0; i < Vars.Count; i++)
                    {
                        d = d.Replace("[" + i + "]", VarArray[i].ToString());
                    }

                    return mp.Parse(d);
                }
                else
                if (s.StartsWith("[") && s.EndsWith("]"))
                {

                    return VarArray[int.Parse(s.TrimStart('[').TrimEnd(']'))];
                }
                else
                {
                    return decimal.Parse(s);
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
