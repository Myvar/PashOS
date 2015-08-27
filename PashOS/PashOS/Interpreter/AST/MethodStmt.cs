using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.AST
{
    public class MethodStmt : Iast
    {
        public int ID { get; set; }
        public int Perams { get; set; }
        public string  RawID { get; set; }
    }
}
