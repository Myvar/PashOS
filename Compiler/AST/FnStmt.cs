using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.AST
{
    public class FnStmt : Iast
    {
        public FnStmt()
        {
            Name = "fnstmt";
            Perams = new List<string>();
        }

        public string CallName { get; set; }
        public List<string> Perams { get; set; }

        public override Iast Parse(ParseTreeNode src)
        {
            var ret = new FnStmt();
            ret.CallName = Iterate(src, "identifier");
            var s2 = FlatTree(IteratePTN(src, "callperams"), ",").Trim(',');


            if (s2.Contains(","))
            {
                foreach (var i in s2.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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
