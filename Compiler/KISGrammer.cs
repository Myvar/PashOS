using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    [Language("KISGrammer", "0.0.1", "KIS")]
    public class KISGrammer : Grammar
    {
        public KISGrammer()
        {
            var number = new NumberLiteral("number");
            var text = new StringLiteral("string", "\"");

            number.DefaultIntTypes = new TypeCode[] { TypeCode.Int32, TypeCode.Int64, NumberLiteral.TypeCodeBigInt };
            var identifier = new IdentifierTerminal("identifier");
            var comment = new CommentTerminal("comment", "#", "\n", "\r");
            base.NonGrammarTerminals.Add(comment);

            var Code = new NonTerminal("code");
            var Statments = new NonTerminal("statments");
            var Value = new NonTerminal("value");
            var ValueString = TerminalFactory.CreateCSharpString("valuestring");

            var SetStmt = new NonTerminal("setstmt");
            var Semicolon = ToTerm(";");

            Value.Rule = number | (ValueString) | identifier | "true" | "false" /* | text */;

            

            //StateMents:
            SetStmt.Rule = identifier + "=" + Value + Semicolon;

            Statments.Rule = SetStmt | comment;


            Code.Rule = MakePlusRule(Code, NewLine, Statments);
            LanguageFlags = LanguageFlags.NewLineBeforeEOF;
            //code := Statment {statment}
            this.Root = Code;
        }
    }
}