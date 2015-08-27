using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class KISCompiler
    {
        public string PashAsm { get; set; }

        public KISCompiler()
        {

        }

        public void Compile(string src)
        {
            var parsed = KISParser.Parse(src);
            
            if(ErrorStack.Errors.Count == 0)
            {
                var ast = Astifi.AstIt(parsed);

            }
            else
            {
                foreach (var i in ErrorStack.Errors)
                {
                    Console.WriteLine(i);
                }
            }

        }
    }
}
