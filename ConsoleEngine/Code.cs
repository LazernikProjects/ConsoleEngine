using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    [Serializable]
    public class Code
    {
        public string Name { get; set; }
        public int IntArg1 { get; set; }
        public int IntArg2 { get; set; }
        public string StrArg1 { get; set; }
        public string StrArg2 { get; set; }

        public static int index = 0;

        public Code (string name, int intArg1, int intArg2, string strArg1, string strArg2)
        {
            Name = name;
            IntArg1 = intArg1;
            IntArg2 = intArg2;
            StrArg1 = strArg1;
            StrArg2 = strArg2;
        }
    }
}
