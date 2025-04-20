using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    [Serializable]
    public class Scene
    {
        public  int X { get; set; } = 0;
        public  int Y { get; set; } = 0;
        public  string objTexture { get; set; } = "{}";
        public  string objColorFG { get; set; } = "black";
        public  string objColorBG { get; set; } = "yellow";
        public  string fieldTexture { get; set; } = "[]";
        public  string fieldColorFG { get; set; } = "darkGray";
        public  string fieldColorBG { get; set; } = "black";
        public  string fillTexture { get; set; } = "()";
        public string renderType { get; set; } = "default";

        public bool drawField = true;
        public int objX = 0;
        public int objY = 0;
        public void Render()
        {
            Console.Clear();

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
            Console.WriteLine();
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
                case ("darkGray"):
                    Console.ForegroundColor = ConsoleColor.DarkGray;
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
                case ("darkGray"):
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
