
using PashOS.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine en = new Engine();
            en.Load(File.ReadAllText(args[0]), false);
            en.ParseHeader(File.ReadAllText(args[1]));

        }
    }
}
