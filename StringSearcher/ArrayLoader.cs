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
        public Dictionary<string, List<string>> readData(string fileName)
        {
            string line;
            Dictionary<string, List<string>> res = new Dictionary<string, List<string>>();
            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                string sortedLine = StringSorter.sortedString(line);
                if (!res.ContainsKey(sortedLine))
                {
                    res.Add(sortedLine, new List<string>());
                }

                res[sortedLine].Add(line);
            }

            file.Close();
            return res;
        }

     }
}
