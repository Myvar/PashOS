using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.AST
{
    public class CallStmt : Iast
    {
        public CallStmt()
        {
            Name = "callstmt";
            Perams = new List<string>();
        }

        public string Path { get; set; }
        public List<string> Perams{ get; set; }

        public override Iast Parse(ParseTreeNode src)
        {
            CallStmt ret = new CallStmt();
            var s1 = FlatTree(IteratePTN(src, "callpath"), ".").Trim('.');
            var s2 = FlatTree(IteratePTN(src, "callperams"), ",").Trim(',');

            if (s1.Contains("."))
            {
                ret.Path = s1.Replace("." + s1.Split('.').Last(), "::") + s1.Split('.').Last();
            }
            else
            {
                ret.Path = s1;
            }

            if (s2.Contains(","))
            {
                foreach(var i in s2.Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    ret.Perams.Add(i);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(s2))
                {
                    ret.Perams.Add(s2);
                }
            }


            return ret;
        }
    }
}
