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
                    
                    if(i.Contains("="))
                    {
                        //Set Stmt
                        //[0] = Error::Panic
                        string[] s1 = i.Split('=');
                        string val = i.Remove(0, GetLoc(i , '=')).Trim();
                        int id = int.Parse(s1[0].Trim().TrimStart('[').TrimStart('{').TrimEnd('}').TrimEnd(']'));
                        ret.Add(new SetStmt() { ID = id, Value = val, RawID = s1[0].Trim() });
                    }
                    else
                    {
                        //{0} (2):
                        //Method Stmt
                        string[] s1 = i.Split(' ');
                        var id = int.Parse(s1[0].Trim().TrimStart('{').TrimEnd('}'));
                        var perams = int.Parse(s1[1].Trim().TrimStart('(').TrimEnd(')'));
                        ret.Add(new MethodStmt() { ID = id, Perams = perams, RawID = s1[0] });
                    }
                }
                if(i.StartsWith("MP"))
                {
                    //MP Stmt
                    

                }
                if (i.StartsWith("CALL"))
                {
                    //CALL Stmt
                }
                if (i.StartsWith("RET"))
                {
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
