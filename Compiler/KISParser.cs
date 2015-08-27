using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class KISParser
    {

        public static ParseTreeNode Parse(string src)
        {
            if (IsValid(src, new KISGrammer()))
            {
                var x = GetRoot(src, new KISGrammer());

                // DispTree(x, 100);
                return x;
            }
            else
            {
                LanguageData language = new LanguageData(new KISGrammer());

                Parser parser = new Parser(language);

                ParseTree parseTree = parser.Parse(src);

                foreach (var i in parseTree.ParserMessages)
                {
                    ErrorStack.Add(i.Message + " on line: " + (i.Location.Line + 1) + " Column: " + (i.Location.Column + 1) + " at Position: " + (i.Location.Position + 1));
                }

                return null;
            }
        }


        #region BasicIrony

        public static void DispTree(ParseTreeNode node, int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("  ");
            Console.WriteLine(node);

            foreach (ParseTreeNode child in node.ChildNodes)
                DispTree(child, level + 1);

        }

        public static ParseTreeNode GetRoot(string sourceCode, Grammar grammar)
        {

            LanguageData language = new LanguageData(grammar);

            Parser parser = new Parser(language);

            ParseTree parseTree = parser.Parse(sourceCode);

            ParseTreeNode root = parseTree.Root;

            return root;

        }

        public static bool IsValid(string sourceCode, Grammar grammar)
        {

            LanguageData language = new LanguageData(grammar);

            Parser parser = new Parser(language);

            ParseTree parseTree = parser.Parse(sourceCode);

            ParseTreeNode root = parseTree.Root;

            return root != null;

        }

        #endregion
    }
}
