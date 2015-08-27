using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.AST
{
    public abstract class Iast
    {
        public string Name { get; set; }

        public bool IsValid(ParseTreeNode src)
        {
            return src.Term?.Name == Name;
        }

        public string  Iterate (ParseTreeNode src, string name)
        {
            string ret = "";

            if (src.Term?.Name == name)
            {
                return src.Token?.ValueString;
            }
            else
            {
                foreach(var i in src.ChildNodes)
                {
                    var z = Iterate(i, name);
                    if(z != null)
                    {
                        return z;
                    }
                }
            }

            return null;
        }

        public abstract Iast Parse(ParseTreeNode src);

    }
}
