using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter
{
    public class Library
    {
        public string Name { get; set; }
        public Lib.Dictionary<string, Delegate> Functions = new Lib.Dictionary<string, Delegate>();
    }
}
