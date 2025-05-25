using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class FillOp : Operator
    {
        string Type = "code";
        public string colorFG { get; set; }
        public string colorBG { get; set; }

        public override void Display(int level)
        {

            DrawLevel(level);
            Editor.CodeV("fill", "", $"[{colorFG}, {colorBG}]", 0);
        }
        public override void Run(Scene scene)
        {
            Fill.fill.Add(new(Engine.project.scene.objX, Engine.project.scene.objY, colorFG, colorBG));
            scene.Render();
        }
        public override void Input(string name)
        {
            if (name == "fill")
            {
                colorFG = Editor.StrRead("ColorFG");
                colorBG = Editor.StrRead("ColorBG");
            }
            else if (name == "fillD")
            {
                colorFG = "black";
                colorBG = "white";
            }
        }
    }
}
