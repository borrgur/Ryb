using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace StringSearcher
{
    class ArrayLoader
    {
        public Dictionary<string, string> readData(string fileName)
        {
            string line;
            Dictionary<string, string> res = new Dictionary<string, string>();
            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                string sortedLine = StringSorter.sortedString(line);
                if (!res.ContainsKey(sortedLine))
                {
                    res.Add(sortedLine, line);
                }
            }

            file.Close();
            return res;
        }

     }
}
