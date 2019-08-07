using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayLoader = new ArrayLoader();
            Dictionary<string, List<string>> data = arrayLoader.readData("./data.txt");
            for (;;)
            {
                Console.Write("Enter 5-letters-long string ");
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    break;
                }

                if (str.Length != 5)
                {
                    Console.WriteLine("Wrong string, must be  5-letters-long");
                    continue;
                }

                string sortedStr = StringSorter.sortedString(str);
                if (data.ContainsKey(sortedStr))
                {
                    Console.Write("String {0} matched to strings: ", str);
                    foreach (string s in data[sortedStr])
                    {
                        Console.Write("{0} ", s);
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("String {0} not found", str);
                }
            }
        }
    }
}
