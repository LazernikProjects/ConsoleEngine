using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Packager
    {
        public static void Package()
        {
            string answer = null;
            Console.Clear();
            Text.Warning("Эта функция в процессе разработки!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"ConsoleEngine Packager - Version {Engine.packagerVersion}");
            Console.WriteLine("Выберите язык программирования:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. C#");
            Console.ForegroundColor = ConsoleColor.White;
            answer = Console.ReadLine();
            switch (answer)
            {
                case ("C#" or "1"):
                    CSharpPackage();
                    break;
                default:
                    Text.CriticalError("Неверное значение");
                    break;
            }
        }
        public static void CSharpPackage()
        {
            string Code = "//Project created with ConsoleEngine by LazernikProjects\n" +
                $"//Engine version: {Engine.version}\n" +
                $"//CEL version: {Engine.versionCEL}\n" +
                $"//Packager version: {Engine.packagerVersion}\n" +
                $"int objX = 0;\nint objY = 0;\n" +
                "int X = 0;\nint Y = 0;\nstring objTexture = \"{}\";\nstring objColorFG = \"black\";\nstring objColorBG = \"yellow\";\r\n" +
                "string fieldTexture = \"[]\";\r\nstring fieldColorFG = \"darkGray\";\r\nstring fieldColorBG = \"black\";\r\n" +
                "string fillTexture = \"()\";\r\nstring renderType = \"default\";" +
                "void Render(){}";
            File.WriteAllText($"C:\\ConsoleEngine\\Projects\\packagerTest\\Program.cs", $"{Code}");
        }
    }
}
