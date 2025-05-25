using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class Repeat : Operator
    {
        string Type = "code";
        public OperatorValue Count { get; set; } = new OperatorValue();
        public List<Operator> Operators = new List<Operator>();
        public override void Display(int level)
        {
            DrawLevel(level);
            Editor.CodeV("repeat", $"({Count.Value}", "", 0);
        }
        public override void Run(Scene scene)
        {
            for (int i = 0; i < Count.Value; i++)
            {
                foreach (Operator op in Operators)
                {
                    op.Run(scene);
                }
            }
        }
        public override void Input(string name)
        {
            Count = Editor.ValueRead("Count");
            while (true)
            {
                string nameR = Editor.StrRead("Name");
                if (nameR == "e")
                {
                    break;
                }
                else { Editor.CommandWrite(Operators, nameR); }
            }
        }
    }
}
