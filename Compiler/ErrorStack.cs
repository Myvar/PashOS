using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class ErrorStack
    {
        public static List<string> Errors = new List<string>();

        public static void Add(string s)
        {
            Errors.Add(s);
        }

    }
}
