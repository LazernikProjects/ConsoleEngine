using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleEngine
{
    [Serializable]
    public class Scene
    {
        public  int X { get; set; } = 0;
        public  int Y { get; set; } = 0;
        public string objTexture { get; set; } = "{}";
        public string objColorFG { get; set; } = "black";
        public string objColorBG { get; set; } = "yellow";
        public string fieldTexture { get; set; } = "[]";
        public string fieldColorFG { get; set; } = "darkgray";
        public string fieldColorBG { get; set; } = "black";
        public string fillTexture { get; set; } = "()";
        public string renderType { get; set; } = "default";
        public string ProjectSaveName { get; set; }

        public bool drawField = true;
        public int objX = 0;
        public int objY = 0;
        public void Render()
        {
            Console.Clear();
            if (Compiler.text != "" & Compiler.text != null)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Output: {Compiler.text}");
            }
            for (int currentY = 1; currentY <= Y; currentY++)
            {
                for (int currentX = 1; currentX <= X; currentX++)
                {
                    drawField = true;

                    if (currentX == objX & currentY == objY)
                    {
                        Color(objColorFG, objColorBG);
                        Console.Write(objTexture);
                        drawField = false;
                    }
                    for (int fillI = 0; fillI < Fill.fill.Count; fillI++)
                    {
                        if (currentX == Fill.fill[fillI].X & currentY == Fill.fill[fillI].Y)
                        {
                            if (currentX != objX || currentY != objY)
                            {
                                Color(Fill.fill[fillI].ColorFG, Fill.fill[fillI].ColorBG);
                                Console.Write(fillTexture);
                                drawField = false;
                            }
                        }
                    }
                    if (drawField == true)
                    {
                        Color(fieldColorFG, fieldColorBG);
                        Console.Write(fieldTexture);
                    }
                }
                Console.WriteLine();
            }
            try
            {
                if (renderType == "wait" || Compiler.compilerStarted == false)
                {
                    if (Engine.project.code.Count > 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"Fill.Count: [{Fill.fill.Count}]");
                        Console.Write(" - Render mode: ");
                        if (renderType == "default")
                            Console.ForegroundColor = ConsoleColor.White;
                        if (renderType == "wait")
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        if (renderType == "fast")
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(renderType);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" - Code line: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(Compiler.codeI);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); Text.Enter(); }
        }
        public static void Color(string FG, string BG)
        {
            switch (FG)
            {
                case ("white"):
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("black"):
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case ("red"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ("yellow"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ("green"):
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ("blue"):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case ("gray"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case ("magenta"):
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case ("cyan"):
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case ("darkgray"):
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case ("darkred"):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ("darkyellow"):
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case ("darkgreen"):
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case ("darkblue"):
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case ("darkmagenta"):
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case ("darkcyan"):
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            switch (BG)
            {
                case ("white"):
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case ("black"):
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("red"):
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case ("yellow"):
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case ("green"):
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case ("blue"):
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case ("gray"):
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case ("magenta"):
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case ("cyan"):
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case ("darkgray"):
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case ("darkred"):
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case ("darkyellow"):
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case ("darkgreen"):
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case ("darkblue"):
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case ("darkmagenta"):
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case ("darkcyan"):
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
