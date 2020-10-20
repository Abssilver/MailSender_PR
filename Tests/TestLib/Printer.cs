using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib
{
    public class Printer
    {
        private string _prefix;
        public Printer(string prefix)
        {
            _prefix = prefix;
        }
        public virtual void Print(string message)
        {
            Console.WriteLine($"{_prefix}{message}");
        }
    }
    internal class InternalPrinter : Printer
    {
        public int Value { get; set; }
        public InternalPrinter(): base("Internal")
        {
        }
        public virtual void Print(string message)
        {
            Console.WriteLine($"Internal {Value}");
            base.Print(message);
        }
    }
}
