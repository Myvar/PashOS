using PashOS.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS
{
    public class KThread
    {

        public ThreadState State = ThreadState.Unstarted;

        private static int _ID = -1;
        public int ID { get; }  = ++_ID;

        public bool Running { get; private set; } = false;
        
        public Process Parent;

        private Engine engine;
        
        public KThread(Process Parent)
        {
            this.Parent = Parent;
            Kernel.Pool.Add(this);
        }

        public void Load(string src)
        {
            if (!Running || engine.ProgramHasEnded)
            {
                engine = new Engine();
                engine.Load(src);
            }
            else throw new Exception("Cannot override a thread with running code, please stop it first...");
        }

        public void Step()
        {
            if (Running && !engine.ProgramHasEnded)
                engine.Step();
        }

        //May want to execute some code when the thread is halted or started...

        public void Start()
        {
            Running = true;
        }

        public void Halt()
        {
            Running = false;
        }
    }

    public enum ThreadState
    {
        Running = 0,
        Unstarted = 8,
        Suspended = 16,
        Aborted = 32
    }
}
