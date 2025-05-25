using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    public abstract class Operator
    {
        public string Type = "unknown";
        public abstract void Run(Scene scene);
        public abstract void Display(int level);
        protected void DrawLevel(int level)
        {
            Console.WriteLine();
            for (int l = 0; l < level; l++)
            {
                Console.Write(" ");
            }
        }
        public abstract void Input(string name);
    }
}
