using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS
{
    public class Process
    {
        public List<KThread> Threads = new List<KThread>();

        public KThread MainThread
        {
            get { return Threads[0]; }
        }

        public Process (string src)
        {
            Threads.Add(new KThread(this));
            MainThread.Load(src);
            Threads.Add(MainThread);
        }

        private Process ()
        {

        }

        public static Process Purge(KThread thread)
        {
            if (thread.Parent.MainThread != thread)
            {
                Process process = new Process();
                process.Threads.Add(thread);

                return process;
            }
            else throw new Exception("Cannot Purge a Main Thread...");
        }

        public void Start()
        {
            if (!MainThread.Running) MainThread.Start();
        }

        public void Halt()
        {
            for (int i = 0; i < Threads.Count; i++)
            {
                Threads[i].Halt();
            }
        }
    }
}