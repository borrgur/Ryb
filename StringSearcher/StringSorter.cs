using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearcher
{
    static class StringSorter
    {
        public static string sortedString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            string sorted = new string(chars);
            return sorted;
        }
    }
}
