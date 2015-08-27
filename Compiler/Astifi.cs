using Compiler.AST;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class Astifi
    {
        private static List<Iast> Statments = new List<Iast>()
        {
            new SetStmt()
        };

        public static  List<Iast> AstIt (ParseTreeNode src)
        {
            var ret = new List<Iast>();
            foreach(var i in src.ChildNodes)
            {
                foreach(var x in Statments)
                {
                    if(x.IsValid(i))
                    {
                        ret.Add(x.Parse(i));
                        break;
                    }
                }
            }

            return ret;
        }

    }
}
