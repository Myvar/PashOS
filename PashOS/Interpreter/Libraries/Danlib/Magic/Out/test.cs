using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.Libraries.Danlib.Magic.Out
{
    public class test : Library
    {
        public test()
        {
            Name = "test";
            Functions.Add("ayylmao", new Action(ayylmao));
        }

        private static void ayylmao()
        {
            Console.WriteLine("AYYYYY LMAOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        }
    }
}
