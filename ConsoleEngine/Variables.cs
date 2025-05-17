using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Variables
    {
        public static List<Variables> var = new List<Variables>();

        public string Name;
        public string Type;
        public int Value;
        public int ID;
        public Variables(string name, string type, int value, int id)
        {
            Name = name;
            Type = type;
            Value = value;
            ID = id;
        }
    }
}
