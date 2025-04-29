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
        public string Type { get; set; }
        public int IntArg1 { get; set; }
        public int IntArg2 { get; set; }
        public string StrArg1 { get; set; }
        public string StrArg2 { get; set; }
        public Code (string name, string type, int intArg1, int intArg2, string strArg1, string strArg2)
        {
            Name = name;
            Type = type;
            IntArg1 = intArg1;
            IntArg2 = intArg2;
            StrArg1 = strArg1;
            StrArg2 = strArg2;
        }
    }
}
