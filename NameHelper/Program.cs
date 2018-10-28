using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = new FormatRulesLoader().Load();
            var formatter = new Formatter(rules);
            Console.WriteLine(formatter.Process(args));
            Console.ReadKey();
        }
    }
}
