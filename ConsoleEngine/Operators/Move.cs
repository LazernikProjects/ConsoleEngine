using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class Move : Operator
    {
        public OperatorValue X { get; set; } = new OperatorValue();
        public OperatorValue Y { get; set; } = new OperatorValue();

        public override void Display(int level)
        {
            DrawLevel(level);
            Editor.CodeV("move", $"({X.Value}, {Y.Value})", "", 0);
        }
        public override void Run(Scene scene)
        {
            scene.objX += X.Value; 
            scene.objY += Y.Value;
            scene.Render();
        }
        public override void Input(string name)
        {
            if (name == "moveX")
            { X = Editor.ValueRead("X"); }
            else if (name == "moveY")
            { Y = Editor.ValueRead("Y"); }
            else if (name == "move")
            {
                X = Editor.ValueRead("X");
                Y = Editor.ValueRead("Y"); 
            }
        }
    }
}