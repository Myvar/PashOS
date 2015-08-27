using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.Meta
{

    public class MetaPackage
    {
        public MetaVariable[] Variables;
        public MetaFunction[] Functions;
    }

    public class MetaInfo
    {
        
        public static MetaPackage Parse(string[] Code)
        {
            //MetaPackage stores all the functions and variables
            MetaPackage package = new MetaPackage();

            List<MetaFunction> functions = new List<MetaFunction>();
            List<MetaVariable> variables = new List<MetaVariable>();

            //Go through code (line by line) and package the MetaData.
            foreach (string ln in Code)
            {
                if (ln.ToCharArray()[0] == '{')
                {
                    //Function

                    MetaFunction f = new MetaFunction();

                    int ID = int.Parse(ln.Split('}')[0].Remove(0));
                    f.ID = ID;

                    string[] spr = ln.Split(' ');

                    string Name = spr[2];
                    f.Name = Name;

                    string brk = spr[3].Remove(0).Remove(spr[3].Length);


                    string[] Types = brk.Split(',');
                    f.Types = Types;

                    string[] Tags = spr[4].Remove(0).Remove(spr[4].Length).Split(',');
                    f.Tags = Tags;
                    
                    functions.Add(f);
                }
                else if (ln.ToCharArray()[0] == '[')
                {

                    //Variable

                    MetaVariable v = new MetaVariable();
                    
                    int ID = int.Parse(ln.Split(']')[0].Remove(0));
                    v.ID = ID;

                    string[] spr = ln.Split(' ');

                    string Name = spr[2];
                    v.Name = Name;

                    string Type = spr[3].Remove(0).Remove(spr[3].Length);
                    v.Type = Type;
                }
            }

            package.Functions = functions.ToArray();
            package.Variables = variables.ToArray();

            return package;
        }

    }

    public class MetaVariable
    {
        public int ID;
        public string Name;
        public string Type;
    }

    public class MetaFunction
    {
        public int ID;
        public string Name;
        public string[] Types;
        public string[] Tags;
    }
}
