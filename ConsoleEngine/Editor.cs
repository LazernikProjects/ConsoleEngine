using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Editor
    {
        public static void CodeWrite()
        {
            int intArg1 = 0;
            int intArg2 = 0;
            string strArg1;
            string strArg2;
            int repeatIndex = 0;
            string repeatAnswer = null;

            string writtenCode = Console.ReadLine();
            switch (writtenCode.ToLower())
            {
                case ("pos" or "p"):
                    Console.Write("\\X=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Console.Write("\\Y=");
                    intArg2 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("pos", intArg1, intArg2, null, null));
                    break;
                case ("movex" or "mx"):
                    Console.Write("\\X=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("moveX", intArg1, 0, null, null));
                    break;
                case ("movey" or "my"):
                    Console.Write("\\Y=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("moveY", intArg1, 0, null, null));
                    break;
                case ("filld" or "fd"):
                    Engine.project.code.Add(new("fill", 0, 0, "black", "white"));
                    break;
                case ("fill" or "f"):
                    Console.Write("\\ColorFG = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\ColorBG = ");
                    strArg2 = Console.ReadLine();
                    Engine.project.code.Add(new("fill", 0, 0, strArg1, strArg2));
                    break;
                case ("repeat" or "rp"):
                    Console.Write("\\count=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("repeat", intArg1, 0, null, null));
                    repeatIndex = Engine.project.code.Count - 1;

                    Console.Write("\\command=");
                    repeatAnswer = Console.ReadLine();
                    switch (repeatAnswer)
                    {
                        case ("moveX" or "mx"):
                            Console.Write("\\X=");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveX", intArg1, 0, null, null));
                            break;
                        case ("moveY" or "my"):
                            Console.Write("\\Y=");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveY", intArg1, 0, null, null));
                            break;
                        case ("fillD" or "fd"):
                            Engine.project.code.Add(new("fill", 0, 0, "black", "white"));
                            break;
                        case ("fill" or "f"):
                            Console.Write("\\ColorFG = ");
                            strArg1 = Console.ReadLine();
                            Console.Write("\\ColorBG = ");
                            strArg2 = Console.ReadLine();
                            Engine.project.code.Add(new("fill", 0, 0, strArg1, strArg2));
                            break;
                    }
                    Engine.project.code[repeatIndex].StrArg1 = repeatAnswer;

                    Console.Write("\\command=");
                    repeatAnswer = Console.ReadLine();
                    switch (repeatAnswer)
                    {
                        case ("moveX" or "mx"):
                            Console.Write("\\X=");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveX", intArg1, 0, null, null));
                            break;
                        case ("moveY" or "my"):
                            Console.Write("\\Y=");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveY", intArg1, 0, null, null));
                            break;
                        case ("fillD" or "fd"):
                            Engine.project.code.Add(new("fill", 0, 0, "black", "white"));
                            break;
                        case ("fill" or "f"):
                            Console.Write("\\ColorFG = ");
                            strArg1 = Console.ReadLine();
                            Console.Write("\\ColorBG = ");
                            strArg2 = Console.ReadLine();
                            Engine.project.code.Add(new("fill", 0, 0, strArg1, strArg2));
                            break;
                    }
                    Engine.project.code[repeatIndex].StrArg2 = repeatAnswer;
                    break;
                case ("clear" or "c"):
                    Engine.project.code.Add(new("clear", 0, 0, null, null));
                    break;
                case ("fillwithpos" or "fwp"):
                    Console.Write("\\ColorFG = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\ColorBG = ");
                    strArg2 = Console.ReadLine();
                    Console.Write("\\X=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Console.Write("\\Y=");
                    intArg2 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("fillWithPos", intArg1, intArg2, strArg1, strArg2));
                    break;
                case ("changerender" or "cr"):
                    Console.Write("\\type=");
                    switch (Console.ReadLine())
                    {
                        case ("default" or "d"):
                            strArg1 = "default";
                            break;
                        case ("wait" or "w"):
                            strArg1 = "wait";
                            break;
                        case ("fast" or "f"):
                            strArg1 = "fast";
                            break;
                        default:
                            strArg1 = "default";
                            Text.Error("Неверное значение (Установлено значение 'default')");
                            break;
                    }
                    Engine.project.code.Add(new("changeRender", 0, 0, strArg1, null));
                    break;
                case ("texture" or "t"):
                    Console.Write("\\Target = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\Texture = ");
                    strArg2 = Console.ReadLine();
                    Engine.project.code.Add(new("texture", 0, 0, strArg1, strArg2));
                    break;
                case ("colorfg" or "cfg"):
                    Console.Write("\\Target = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\Color (FG) = ");
                    strArg2 = Console.ReadLine();
                    Engine.project.code.Add(new("colorFG", 0, 0, strArg1, strArg2));
                    break;
                case ("colorbg" or "cbg"):
                    Console.Write("\\Target = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\Color (BG) = ");
                    strArg2 = Console.ReadLine();
                    Engine.project.code.Add(new("colorBG", 0, 0, strArg1, strArg2));
                    break;
                case ("scenesize" or "ss"):
                    Console.Write("\\X=");
                    intArg1 = int.Parse(Console.ReadLine());
                    Console.Write("\\Y=");
                    intArg2 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("sceneSize", intArg1, intArg2, null, null));
                    break;
                case ("wait" or "w"):
                    Engine.project.code.Add(new("wait", 0, 0, null, null));
                    break;
                case ("/start" or "start" or "/s"):
                    Compiler.Start(Engine.project);
                    break;
                case ("/info" or "/i"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("- Данные проекта:");
                    Console.WriteLine($"Название: {Engine.project.name}");
                    Console.WriteLine($"Размер сцены: X-{Engine.project.scene.X}, Y-{Engine.project.scene.Y}");
                    Console.WriteLine($"Количество строчек кода: {Engine.project.code.Count}");
                    Console.WriteLine($"Кол-во объектов (fill): {Fill.fill.Count}");
                    Console.WriteLine($"Тип рендера: {Engine.project.scene.renderType}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    break;
                case ("/p.render"):
                    Console.Write("\\type=");
                    switch (Console.ReadLine())
                    {
                        case ("default" or "d"):
                            Engine.project.scene.renderType = "default";
                            break;
                        case ("wait" or "w"):
                            Engine.project.scene.renderType = "wait";
                            break;
                        case ("fast" or "f"):
                            Engine.project.scene.renderType = "fast";
                            break;
                        default:
                            Engine.project.scene.renderType = "default";
                            Text.Error("Неверное значение (Установлено значение 'default')");
                            break;
                    }
                    break;
                case ("/p.scenesize"):
                    Console.Write("\\X=");
                    Engine.project.scene.X = int.Parse(Console.ReadLine());
                    Console.Write("\\Y=");
                    Engine.project.scene.Y = int.Parse(Console.ReadLine());
                    break;
                case ("/p.color"):
                    Console.Write("\\Target = ");
                    strArg1 = Console.ReadLine();
                    if (strArg1 == "obj")
                    {
                        Console.Write("\\Color (FG) = ");
                        strArg2 = Console.ReadLine();
                        Engine.project.scene.objColorFG = strArg2;
                        Console.Write("\\Color (BG) = ");
                        strArg2 = Console.ReadLine();
                        Engine.project.scene.objColorBG = strArg2;
                    }
                    if (strArg1 == "field")
                    {
                        Console.Write("\\Color (FG) = ");
                        strArg2 = Console.ReadLine();
                        Engine.project.scene.fieldColorFG = strArg2;
                        Console.Write("\\Color (BG) = ");
                        strArg2 = Console.ReadLine();
                        Engine.project.scene.fieldColorBG = strArg2;
                    }
                    else 
                    { Text.Error("Выбран неверный объект"); }
                        break;
                case ("/p.texture"):
                    Text.Error("Команда недоступна");
                    break;
                case ("/p.name"):
                    Console.Write("Новое имя проекта: ");
                    Engine.project.name = Console.ReadLine();
                    break;
                case ("/save" or "/s"):
                    Engine.project.Save();
                    break;
                case ("/help" or "/h"):
                    Project.Help();
                    break;
                case ("/engine"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("- ConsoleEngine:");
                    Console.WriteLine($"by LazernikProjects");
                    Console.WriteLine($"Engine version: {Engine.version}");
                    Console.WriteLine($"CEL version: {Engine.versionCEL}");
                    Console.WriteLine($"Framework: {Engine.dotnet}");
                    Console.WriteLine($"Engine folder: C:\\ConsoleEngine");
                    Console.WriteLine($"GitHub: https://github.com/LazernikProjects/ConsoleEngine");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    break;
                case ("-custom" or "custom" or "-c"):
                    Console.Write("\\Name = ");
                    repeatAnswer = Console.ReadLine();
                    Console.Write("\\IntArg1 = ");
                    intArg1 = int.Parse(Console.ReadLine());
                    Console.Write("\\IntArg2 = ");
                    intArg2 = int.Parse(Console.ReadLine());
                    Console.Write("\\StrArg1 = ");
                    strArg1 = Console.ReadLine();
                    Console.Write("\\StrArg2 = ");
                    strArg2 = Console.ReadLine();
                    Engine.project.code.Add(new(repeatAnswer, intArg1, intArg2, strArg1, strArg2));
                    break;
                case ("-edit" or "edit" or "-e"):
                    Console.Write("\\Line number = ");
                    intArg1 = int.Parse(Console.ReadLine());
                    intArg1--;
                    Console.Write("\\IntArg1 = ");
                    Engine.project.code[intArg1].IntArg1 = int.Parse(Console.ReadLine());
                    Console.Write("\\IntArg2 = ");
                    Engine.project.code[intArg1].IntArg2 = int.Parse(Console.ReadLine());
                    Console.Write("\\StrArg1 = ");
                    Engine.project.code[intArg1].StrArg1 = Console.ReadLine();
                    Console.Write("\\StrArg2 = ");
                    Engine.project.code[intArg1].StrArg2 = Console.ReadLine();
                    break;
                case ("-delete" or "delete" or "-d"):
                    Console.Write("\\Line number = ");
                    intArg1 = int.Parse(Console.ReadLine());
                    Engine.project.code.RemoveAt(intArg1 - 1);
                    break;
                default:
                    CodeWrite();
                    break;
            }
            Engine.project.scene.Render();
            CodeView();
            CodeWrite();
        }
        public static void CodeView()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#Code");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" - Для просмотра всех команд введите /help, для старта программы введите /start");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            for (int codeI = 0; codeI < Engine.project.code.Count; codeI++)
            {
                switch (Engine.project.code[codeI].Name)
                {
                    case ("pos"):
                        CodeV("pos", $"({Engine.project.code[codeI].IntArg1}, {Engine.project.code[codeI].IntArg2})", "");
                        break;
                    case ("moveX"):
                        CodeV("moveX", $"({Engine.project.code[codeI].IntArg1})", "");
                        break;
                    case ("moveY"):
                        CodeV("moveY", $"({Engine.project.code[codeI].IntArg1})", "");
                        break;
                    case ("fill"):
                        CodeV("fill", "", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                    case ("repeat"):
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"repeat({Engine.project.code[codeI].IntArg1})");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($" \\{Engine.project.code[codeI].StrArg1}");
                        Console.WriteLine();
                        Console.Write($" \\{Engine.project.code[codeI].StrArg2}");
                        codeI += 2;
                        break;
                    case ("fillWithPos"):
                        CodeV("fillWithPos", $"({Engine.project.code[codeI].IntArg1}, {Engine.project.code[codeI].IntArg2})", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                    case ("wait"):
                        CodeV("wait", "", "");
                        break;
                    case ("texture"):
                        CodeV("texture", "", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                    case ("colorBG"):
                        CodeV("colorBG", "", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                    case ("colorFG"):
                        CodeV("colorFG", "", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                    case ("clear"):
                        CodeV("clear", "", "");
                        break;
                    case ("sceneSize"):
                        CodeV("sceneSize", $"({Engine.project.code[codeI].IntArg1}, {Engine.project.code[codeI].IntArg2})", "");
                        break;
                    case ("changeRender"):
                        CodeV("changeRender", "", $"[{Engine.project.code[codeI].StrArg1}]");
                        break;
                    default:
                        CodeV($"{Engine.project.code[codeI].Name}", $"({Engine.project.code[codeI].IntArg1}, {Engine.project.code[codeI].IntArg2})", $"[{Engine.project.code[codeI].StrArg1}, {Engine.project.code[codeI].StrArg2}]");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
        public static void CodeV(string name, string arg, string arg2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{arg}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{arg2}");
            //Console.WriteLine();
        }
        public static void CodeEdit()
        {

        }
        public static void CodeDelete()
        {

        }
    }
}
