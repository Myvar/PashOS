using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.AST
{
    public class LogicStmt : Iast
    {
        public string Left { get; set; }
        public string Opcode { get; set; }
        public string Right { get; set; }

        public int ID { get; set; }

    }
}
