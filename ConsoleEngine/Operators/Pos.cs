using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class Pos : Operator
    {
        string Type = "code";
        public int X { get; set; }
        public int Y { get; set; }

        public override void Display(int level)
        {
            DrawLevel(level);
            Editor.CodeV("pos", $"({X}, {Y})", "", 0);
        }
        public override void Run(Scene scene)
        {
            scene.objX = X;
            scene.objY = Y;
            scene.Render();
        }
        public override void Input(string name)
        {
            X = Editor.IntRead("X");
            Y = Editor.IntRead("Y");
        }
    }
}