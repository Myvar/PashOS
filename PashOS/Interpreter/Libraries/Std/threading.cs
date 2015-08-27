using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.Libraries.Std
{
    public class threading : Library
    {


        public threading ()
        {
            Name = "threading";
            Functions.Add("getCurrentThread", new Func<int>(threading.getCurrentThread));
            Functions.Add("getThreadState", new Func<int, int>(threading.getThreadState));
            Functions.Add("getThreadState", new Func<int, int>(threading.getThreadState));
        }

        private static int getCurrentThread()
        {
            return Kernel.currentThread.ID;
        }

        private static int getThreadState(int t)
        {
            return (int)Kernel.Pool[t].State;
        }

        private static int createThread()
        {
            //Needs changing!!!
            return new KThread(Kernel.currentThread.Parent).ID;
        }

        private static void createfakethread()
        {
            Console.WriteLine("Created a fake thread...");
        }
    }
}
