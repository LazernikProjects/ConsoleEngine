using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class Variable : Operator
    {
        string Name;
        int Value;

        public override void Display(int level)
        {
            DrawLevel(level);
            Editor.CodeV("var", $"{Value}", $"{Name}", 0);
        }
        public override void Input(string name)
        {
            Name = Editor.StrRead("Var name");
            Value = Editor.IntRead("Value");
            Variables.var.Add(new(Name, "int", Value, Variables.var.Any() ? Variables.var.Max(v => v.ID) + 1 : 16000));
        }
        public override void Run(Scene scene)
        {
            if (!Variables.var.Any(v => v.Name == Name))
            {
                Variables.var.Add(new(Name, "int", Value, Variables.var.Any() ? Variables.var.Max(v => v.ID) + 1 : 16000));
            }
        }
    }
}
