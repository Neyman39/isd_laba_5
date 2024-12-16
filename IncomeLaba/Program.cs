using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeLaba
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Вывод из файла ===");
            foreach (BasicIncomeType linefile in FactoryIncome.ListToObjects(FactoryIncome.StrFromFiles("3laba.txt")))
            {
                Console.WriteLine(linefile.LineOutput());
            }

            Console.ReadKey();
        }
    }
}
