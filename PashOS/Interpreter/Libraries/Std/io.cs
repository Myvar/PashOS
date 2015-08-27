using PashOS.Interpreter;
using System;

namespace PashOS.Std
{
    public class Out : Library
    {

        public Out()
        {
            Name = "out";
            Functions = new Lib.Dictionary<string, Delegate>();
            Functions.Add("println", new Action<object>(Out.printl));
            Functions.Add("print", new Action<object>(Out.print));
            Functions.Add("printc", new Action<object, string>(Out.printc));
            Functions.Add("printlnc", new Action<object, string>(Out.printlnc));
        }

        private static void print(object s)
        {
            Console.Write(s);
        }

        private static void printl(object s)
        {
            Console.WriteLine(s);
        }

        private static string readl()
        {
            return Console.ReadLine();
        }

        private static void printc(object s, string c)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), c);
            Console.Write(s);
            Console.ResetColor();
        }

        private static void printlnc(object s, string c)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), c);
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}
