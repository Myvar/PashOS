using PashOS.Interpreter.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter
{
    public class Parser
    {

        public Parser()
        {

        }


        private int GetLoc(string src, char what)
        {
            int index = 0;
            foreach(var i in src)
            {
                index++;
                if(i == what)
                {
                    break;
                }
            }

            return index;
        }

        public List<Iast> Parse(string src)
        {
            List<Iast> ret = new List<Iast>();
            foreach(var z in src.Replace("\r\n","\n").Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
            {
                //[100]=MessageBox::Show
                var i = z.Trim();
                if (i.StartsWith("[") || i.StartsWith("{"))
                {
                    //method stmt or a set stmt
                    if (i.Contains("==") || i.Contains("!=") || i.Contains("<") || i.Contains(">") && i.EndsWith("}"))
                    {
                        //Logic stmt
                        //[0] == "Ping"
                        string[] s1 = i.Split(' ');
                        ret.Add(new LogicStmt() { Left = s1[0].Trim(), Opcode = s1[1].Trim(), Right = s1[2].Trim(), ID = int.Parse(s1[3].Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']')) });
                    }
                    else
                    {
                        if (i.Contains("="))
                        {
                            //Set Stmt
                            //[0] = Error::Panic
                            string[] s1 = i.Split('=');
                            string val = i.Remove(0, GetLoc(i, '=')).Trim();
                            int id = int.Parse(s1[0].Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']'));
                            ret.Add(new SetStmt() { ID = id, Value = val, RawID = s1[0].Trim() });
                        }

                        else
                        {
                            //{0} (2):
                            //Method Stmt
                            string[] s1 = i.Split(' ');
                            var id = int.Parse(s1[0].Trim().TrimStart('{').TrimEnd('}'));
                            var perams = int.Parse(s1[1].Trim().Trim(':').TrimStart('(').TrimEnd(')'));
                            ret.Add(new MethodStmt() { ID = id, Perams = perams, RawID = s1[0] });
                        }
                    }
                }
                if(i.StartsWith("MP"))
                {
                    //MP Stmt
                    //MP.0 [0] %1
                    string[] s1 = i.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var index = s1[0].Split('.').Last();
                    var a = s1[1].Trim();
                    var b = s1.Last().Trim();
                    ret.Add(new MPStmt() { MethodID = int.Parse(a.Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']'))
                    , ParameterID = int.Parse(index.Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']'))
                    , Value = b});
                }
                if (i.StartsWith("CALL"))
                { 
                    //CALL Stmt
                    string[] s1 = i.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ret.Add(new CallStmt() { ID = int.Parse(s1[1].Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']')) });
                }
                if (i.Trim() == "RET")
                {
                    ret.Add(new RetStmt());
                    //RET Stmt
                }
                if (i.StartsWith("IMP"))
                {
                    //IMP Stmt
                }
            }


            return ret;
        }

    }
}
