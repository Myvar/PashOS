
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
            en.Execute(File.ReadAllText(args[0]));
            en.ParseHeader(File.ReadAllText(args[1]));

        }
    }
}
