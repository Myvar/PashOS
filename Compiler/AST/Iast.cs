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
           

            if (src.Term?.Name == name)
            {
                if (src.Token != null)
                {
                    return src.Token?.ValueString;
                }
                else
                {
                    return IterateValue(src);
                }
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

        public ParseTreeNode IteratePTN(ParseTreeNode src, string name)
        {
            

            if (src.Term?.Name == name)
            {
                    return src;                
            }
            else
            {
                foreach (var i in src.ChildNodes)
                {
                    var z = IteratePTN(i, name);
                    if (z != null)
                    {
                        return z;
                    }
                }
            }

            return null;
        }

        private string IterateFlatRes = "";

        private void pIterateFlat(ParseTreeNode root, string binder)
        {
            if (root != null)
            {

                if (root?.Token != null)
                {
                    IterateFlatRes += root.Token.ValueString + binder;
                }
                foreach (var i in root?.ChildNodes)
                {
                    pIterateFlat(i, binder);
                }
            }
        }

        public string FlatTree(ParseTreeNode root, string binder = "")
        {
            IterateFlatRes = "";
            pIterateFlat(root, binder);
            return IterateFlatRes;
        }

        public string IterateValue(ParseTreeNode root, bool identifier = false)
        {
            if (root.Term.Name == "string")
            {
                return root.Token.Value.ToString();
            }
            if (root.Term.Name == "qstring")
            {
                return "\"" + IterateValue(root) + "\"";
            }
            if (root.Term.Name == "exspr")
            {                
                return FlatTree(root);
            }

            if (root.Term.Name == "number")
            {
                return root.Token.Value.ToString();
            }

            if (root.Term.Name == "valuestring")
            {
                return "\"" + root.Token.Value.ToString() + "\"";
            }
            if (root.Term.Name == "identifier" && identifier)
            {
                return root.Token.Value.ToString();
            }
            foreach (var i in root.ChildNodes)
            {
                var x = IterateValue(i, identifier);
                if (x != null)
                {
                    return x;
                }
            }
            return null;
        }

        public abstract Iast Parse(ParseTreeNode src);

    }
}
