using PashOS.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Process
{
    public class Process
    {
        public List<KThread> Threads = new List<KThread>();

        public KThread MainThread
        {
            get
            {
                return Threads[0];
            }
        }

        public Process(string Source_Code)
        {
            engine = new Engine(Source_Code);
        }

        public void Start()
        {
            canStep = true;
        }

        public void Stop()
        {
            canStep = false;
        }
    }
}
