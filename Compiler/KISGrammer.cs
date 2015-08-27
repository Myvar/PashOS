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
            var CallStmt = new NonTerminal("callstmt");
            var FnStmt = new NonTerminal("fnstmt");

            var CallPath = new NonTerminal("callpath");
            var CallPerams = new NonTerminal("callperams");

            var fullstop = new NonTerminal("fullstop");
            var comma = new NonTerminal("comma");
            var openb = new NonTerminal("openb");
            var closeb = new NonTerminal("closeb");
            openb.Rule = "{";
            closeb.Rule = "}";
            fullstop.Rule = ".";
            comma.Rule = ",";
            

            CallPath.Rule = MakePlusRule(CallPath, fullstop, identifier);
            CallPerams.Rule = MakePlusRule(CallPerams, comma, Value);

            var Semicolon = ToTerm(";");

            //StateMents:
            SetStmt.Rule = identifier + "=" + Value + Semicolon;
            CallStmt.Rule = (CallPath | identifier) + "(" + (CallPerams | Empty) + ")" + Semicolon;
            FnStmt.Rule = "function" + identifier + "(" + (CallPerams | Empty) + ")";

            Statments.Rule = SetStmt | CallStmt | FnStmt | openb | closeb | Empty;

            var Exspr = new NonTerminal("exspr");
            var Operator = new NonTerminal("operator");
            var ExsprStmt = new NonTerminal("exsprstmt");


            Operator.Rule = ToTerm("/") | "*" | "-" | "+";

            ExsprStmt.Rule = Value + Operator + Value;

            Exspr.Rule = MakePlusRule(Exspr, Operator, ExsprStmt);

            Value.Rule = number | ValueString | identifier | "true" | "false" | Exspr /* | text */;
            Code.Rule = MakePlusRule(Code, NewLine, Statments);
            LanguageFlags = LanguageFlags.NewLineBeforeEOF;
            //code := Statment {statment}
            this.Root = Code;
        }
    }
}