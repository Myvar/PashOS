﻿using PashOS.Interpreter;
using System;

namespace PashOS.Std
{
    public class io : Library
    {
        
        public io ()
        {
            Name = "io";
            Functions = new Lib.Dictionary<string, Delegate>();
            Functions.Add("print", new Action<object>(io.print));
            Functions.Add("printc", new Action<object, string>(io.printc));
            Functions.Add("pause", new Action(io.pause));
            Functions.Add("TestReturn", new Func<int>(testReturn));
        }

        private static void print(object s)
        {
            Console.WriteLine(s);
        }

        private static void printc(object s, string c)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), c);
            Console.WriteLine(s);
            Console.ResetColor();
        }

        private static void pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static int testReturn ()
        {
            return 1;
        }
    }
}