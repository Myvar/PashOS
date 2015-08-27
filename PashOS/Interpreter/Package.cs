using PashOS.Interpreter;
using PashOS.Interpreter.Libraries.Std;
using PashOS.Std;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter
{
    public class Package
    {

        //static
        public static List<Package> Packages = new List<Package>
        {
            new std()
        };

        public static Package getPackage(string name)
        {
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages[i].Name == name) return Packages[i];
            }
            throw new Exception("Unknown package: " + name);
        }


        //public
        public Library getLibrary(string lib)
        {
            for (int i = 0; i < Libraries.Count; i++)
            {
                var z = Libraries[i].Name;
                if (z == lib) return Libraries[i];
            }
            return null;
        }

        public string Name;
        public List<Library> Libraries = new List<Library>();
    }
    public class std : Package
    {
        public std ()
        {
            Name = "std";
            Libraries.Add(new io());
            Libraries.Add(new threading());
        }
    }
}
