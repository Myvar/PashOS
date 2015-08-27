using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.Libraries.Std
{
    public class @string : Library
    {
        public @string()
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
