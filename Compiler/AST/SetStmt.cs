using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Compiler.AST
{
    public class SetStmt : Iast
    {
        public SetStmt()
        {
            Name = "setstmt";
        }

        public string VarName { get; set; }
        public string VarValue { get; set; }

        public override Iast Parse(ParseTreeNode src)
        {
            SetStmt ret = new SetStmt();
            ret.VarName = Iterate(src, "identifier");
            ret.VarValue = Iterate(src, "value");
            return ret;
        }
    }
}
