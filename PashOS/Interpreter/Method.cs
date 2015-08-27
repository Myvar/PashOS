using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter
{
    public class Method
    {
        public int ID { get; set; } = -1;
        public string Name { get; set; }
        public List<string> Perms { get; set; } = new List<string>();

    }
}
