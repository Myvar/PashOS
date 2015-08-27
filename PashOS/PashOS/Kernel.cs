
using PashOS.Interpreter;
using PashOS.Process;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PashOS
{
    public class Kernel : Sys.Kernel
    {
        private Manager manager = new Manager();

        protected override void BeforeRun()
        {
            Console.WriteLine("PashOS created by Emile Bodenhorst and Daniel Jones");

            Process proc;

        }

        protected override void Run()
        {
            manager.Step();
        }
    }
}
