using PashOS.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PashOS
{
    public class Kernel : Sys.Kernel
    {
        public static List<KThread> Pool      = new List<KThread>();
        public static List<Process> Processes = new List<Process>();

        protected override void BeforeRun()
        {
            Console.WriteLine("PashOS created by Emile Badenhorst and Daniel Jones");
            Engine en = new Engine();
            Process BootStrap = new Process("");

        }

        public static KThread currentThread;

        protected override void Run()
        {
            //Step through every thread...
            for (int i = 0; i < Pool.Count; i++)
            {
                currentThread = Pool[i];
                Pool[i].Step();
            }
        }
    }
}
