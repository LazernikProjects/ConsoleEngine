using ConsoleEngine.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleEngine
{
    public class Editor
    {
        public static string commandType = null; //для CodeV()
        public static bool showCode = true;
        public static bool showScene = true;
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
                    intArg1 = IntRead("X");
                    intArg2 = IntRead("Y");
                    Engine.project.code.Add(new("pos", "engine", intArg1, intArg2, null, null));
                    break;
                case ("movex" or "mx"):
                    intArg1 = IntRead("X");
                    Engine.project.code.Add(new("moveX", "engine", intArg1, 0, null, null));
                    break;
                case ("movey" or "my"):
                    intArg1 = IntRead("Y");
                    Engine.project.code.Add(new("moveY", "engine", intArg1, 0, null, null));
                    break;
                case ("move" or "m"):
                    intArg1 = IntRead("X");
                    intArg2 = IntRead("Y");
                    Engine.project.code.Add(new("move", "engine", intArg1, intArg2, null, null));
                    break;
                case ("filld" or "fd"):
                    Engine.project.code.Add(new("fill", "engine", 0, 0, "black", "white"));
                    break;
                case ("fill" or "f"):
                    strArg1 = StrRead("ColorFG");
                    strArg2 = StrRead("ColorBG");
                    Engine.project.code.Add(new("fill", "engine", 0, 0, strArg1, strArg2));
                    break;
                case ("repeat" or "rp"):
                    Text.Warning("Данный цикл устарел, лучше используйте 'nRepeat'");
                    Console.Write("\\Value = ");
                    intArg1 = int.Parse(Console.ReadLine());
                    Engine.project.code.Add(new("repeat", "engine", intArg1, 0, null, null));
                    repeatIndex = Engine.project.code.Count - 1;

                    Console.Write("\\Command = ");
                    repeatAnswer = Console.ReadLine();
                    switch (repeatAnswer)
                    {
                        case ("moveX" or "mx"):
                            Console.Write("\\X = ");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveX", "engine", intArg1, 0, null, null));
                            break;
                        case ("moveY" or "my"):
                            Console.Write("\\Y = ");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveY", "engine", intArg1, 0, null, null));
                            break;
                        case ("fillD" or "fd"):
                            Engine.project.code.Add(new("fill", "engine", 0, 0, "black", "white"));
                            break;
                        case ("fill" or "f"):
                            Console.Write("\\ColorFG = ");
                            strArg1 = Console.ReadLine();
                            Console.Write("\\ColorBG = ");
                            strArg2 = Console.ReadLine();
                            Engine.project.code.Add(new("fill", "engine", 0, 0, strArg1, strArg2));
                            break;
                    }
                    Engine.project.code[repeatIndex].StrArg1 = repeatAnswer;

                    Console.Write("\\Command = ");
                    repeatAnswer = Console.ReadLine();
                    switch (repeatAnswer)
                    {
                        case ("moveX" or "mx"):
                            Console.Write("\\X = ");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveX", "engine", intArg1, 0, null, null));
                            break;
                        case ("moveY" or "my"):
                            Console.Write("\\Y = ");
                            intArg1 = int.Parse(Console.ReadLine());
                            Engine.project.code.Add(new("moveY", "engine", intArg1, 0, null, null));
                            break;
                        case ("fillD" or "fd"):
                            Engine.project.code.Add(new("fill", "engine", 0, 0, "black", "white"));
                            break;
                        case ("fill" or "f"):
                            Console.Write("\\ColorFG = ");
                            strArg1 = Console.ReadLine();
                            Console.Write("\\ColorBG = ");
                            strArg2 = Console.ReadLine();
                            Engine.project.code.Add(new("fill", "engine", 0, 0, strArg1, strArg2));
                            break;
                    }
                    Engine.project.code[repeatIndex].StrArg2 = repeatAnswer;
                    break;
                case ("clear" or "c"):
                    Engine.project.code.Add(new("clear", "engine", 0, 0, null, null));
                    break;
                case ("fillwithpos" or "fwp"):
                    intArg1 = IntRead("X");
                    intArg2 = IntRead("Y");
                    strArg1 = StrRead("ColorFG");
                    strArg2 = StrRead("ColorBG");
                    Engine.project.code.Add(new("fillWithPos", "engine", intArg1, intArg2, strArg1, strArg2));
                    break;
                case ("fillwithposd" or "fwpd"):
                    intArg1 = IntRead("X");
                    intArg2 = IntRead("Y");
                    Engine.project.code.Add(new("fillWithPos", "engine", intArg1, intArg2, "black", "white"));
                    break;
                case ("changerender" or "cr"):
                    switch (StrRead("Type"))
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
                            Text.Enter();
                            break;
                    }
                    Engine.project.code.Add(new("changeRender", "engine", 0, 0, strArg1, null));
                    break;
                case ("texture" or "t"):
                    strArg1 = StrRead("Target");
                    strArg2 = StrRead("Texture");
                    Engine.project.code.Add(new("texture", "engine", 0, 0, strArg1, strArg2));
                    break;
                case ("colorfg" or "cfg"):
                    strArg1 = StrRead("Target");
                    strArg2 = StrRead("ColorFG");
                    Engine.project.code.Add(new("colorFG", "engine", 0, 0, strArg1, strArg2));
                    break;
                case ("colorbg" or "cbg"):
                    strArg1 = StrRead("Target");
                    strArg2 = StrRead("ColorBG");
                    Engine.project.code.Add(new("colorBG", "engine", 0, 0, strArg1, strArg2));
                    break;
                case ("scenesize" or "ss"):
                    intArg1 = IntRead("X");
                    intArg2 = IntRead("Y");
                    Engine.project.code.Add(new("sceneSize", "engine", intArg1, intArg2, null, null));
                    break;
                case ("wait" or "w"):
                    Engine.project.code.Add(new("wait", "engine", 0, 0, null, null));
                    break;
                case ("text" or "tx"):
                    strArg1 = StrRead("Text");
                    Engine.project.code.Add(new("text", "engine", 0, 0, strArg1, null));
                    break;
                case ("var" or "v"):
                    strArg1 = StrRead("Name");
                    intArg1 = IntRead("Value");
                    Variables.var.Add(new(strArg1, "int", intArg1, 16000 + Variables.var.Count));
                    Engine.project.code.Add(new("var", "engine-var", intArg1, 0, strArg1, "int"));
                    break;
                case ("var set"):
                    strArg1 = StrRead("Name");
                    intArg1 = IntRead("Value");
                    Engine.project.code.Add(new("varSet", "engine", intArg1, 0, strArg1, null));
                    break;
                case ("var change"):
                    strArg1 = StrRead("Name");
                    strArg2 = StrRead("Operation");
                    intArg1 = IntRead("Value");
                    Engine.project.code.Add(new("varChange", "engine", intArg1, 0, strArg1, strArg2));
                    break;
                case ("nrepeat" or "nr"):
                    Console.Write("\\Value = ");
                    intArg1 = int.Parse(Console.ReadLine());
                    int repeatCommandsCount = 0;
                    Engine.project.code.Add(new("nRepeat", "engine", intArg1, 0, null, null));
                    int g = Engine.project.code.Count - 1;
                    Console.WriteLine("Для завершения цикла напишите 'exit'");
                    for (bool e = false; e == false;)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\\Command = ");
                        repeatAnswer = Console.ReadLine();
                        if (repeatAnswer == "exit")
                        { e = true; repeatCommandsCount--; }
                        else
                        {
                            switch (repeatAnswer.ToLower())
                            {
                                case ("pos" or "p"):
                                    intArg1 = IntRead("X");
                                    intArg2 = IntRead("Y");
                                    Engine.project.code.Add(new("pos", "engine", intArg1, intArg2, null, null));
                                    break;
                                case ("movex" or "mx"):
                                    intArg1 = IntRead("X");
                                    Engine.project.code.Add(new("moveX", "engine", intArg1, 0, null, null));
                                    break;
                                case ("movey" or "my"):
                                    intArg1 = IntRead("Y");
                                    Engine.project.code.Add(new("moveY", "engine", intArg1, 0, null, null));
                                    break;
                                case ("move" or "m"):
                                    intArg1 = IntRead("X");
                                    intArg2 = IntRead("Y");
                                    Engine.project.code.Add(new("move", "engine", intArg1, intArg2, null, null));
                                    break;
                                case ("filld" or "fd"):
                                    Engine.project.code.Add(new("fill", "engine", 0, 0, "black", "white"));
                                    break;
                                case ("fill" or "f"):
                                    strArg1 = StrRead("ColorFG");
                                    strArg2 = StrRead("ColorBG");
                                    Engine.project.code.Add(new("fill", "engine", 0, 0, strArg1, strArg2));
                                    break;
                                case ("clear" or "c"):
                                    Engine.project.code.Add(new("clear", "engine", 0, 0, null, null));
                                    break;
                                case ("fillwithpos" or "fwp"):
                                    intArg1 = IntRead("X");
                                    intArg2 = IntRead("Y");
                                    strArg1 = StrRead("ColorFG");
                                    strArg2 = StrRead("ColorBG");
                                    Engine.project.code.Add(new("fillWithPos", "engine", intArg1, intArg2, strArg1, strArg2));
                                    break;
                                case ("fillwithposd" or "fwpd"):
                                    intArg1 = IntRead("X");
                                    intArg2 = IntRead("Y");
                                    Engine.project.code.Add(new("fillWithPos", "engine", intArg1, intArg2, "black", "white"));
                                    break;
                                case ("changerender" or "cr"):
                                    switch (StrRead("Type"))
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
                                            Text.Enter();
                                            break;
                                    }
                                    Engine.project.code.Add(new("changeRender", "engine", 0, 0, strArg1, null));
                                    break;
                                case ("texture" or "t"):
                                    strArg1 = StrRead("Target");
                                    strArg2 = StrRead("Texture");
                                    Engine.project.code.Add(new("texture", "engine", 0, 0, strArg1, strArg2));
                                    break;
                                case ("colorfg" or "cfg"):
                                    strArg1 = StrRead("Target");
                                    strArg2 = StrRead("ColorFG");
                                    Engine.project.code.Add(new("colorFG", "engine", 0, 0, strArg1, strArg2));
                                    break;
                                case ("colorbg" or "cbg"):
                                    strArg1 = StrRead("Target");
                                    strArg2 = StrRead("ColorBG");
                                    Engine.project.code.Add(new("colorBG", "engine", 0, 0, strArg1, strArg2));
                                    break;
                                case ("scenesize" or "ss"):
                                    intArg1 = IntRead("X");
                                    intArg2 = IntRead("Y");
                                    Engine.project.code.Add(new("sceneSize", "engine", intArg1, intArg2, null, null));
                                    break;
                                case ("wait" or "w"):
                                    Engine.project.code.Add(new("wait", "engine", 0, 0, null, null));
                                    break;
                                case ("text" or "tx"):
                                    strArg1 = StrRead("Text");
                                    Engine.project.code.Add(new("text", "engine", 0, 0, strArg1, null));
                                    break;
                                case ("var" or "v"):
                                    strArg1 = StrRead("Name");
                                    intArg1 = IntRead("Value");
                                    Variables.var.Add(new(strArg1, "int", intArg1, 16000 + Variables.var.Count));
                                    Engine.project.code.Add(new("var", "engine-var", intArg1, 0, strArg1, "int"));
                                    break;
                                case ("var set"):
                                    strArg1 = StrRead("Name");
                                    intArg1 = IntRead("Value");
                                    Engine.project.code.Add(new("varSet", "engine", intArg1, 0, strArg1, null));
                                    break;
                                case ("var change"):
                                    strArg1 = StrRead("Name");
                                    strArg2 = StrRead("Operation");
                                    intArg1 = IntRead("Value");
                                    Engine.project.code.Add(new("varChange", "engine", intArg1, 0, strArg1, strArg2));
                                    break;
                            }
                            Engine.project.code[Engine.project.code.Count - 1].Type = "engine-repeat";
                        }
                        repeatCommandsCount += 1;
                    }
                    Engine.project.code[g].IntArg2 = repeatCommandsCount;
                    break;
                case ("/start" or "start" or "/s"):
                    Compiler.Start(Engine.project);
                    break;
                case ("/info" or "info" or "/i"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("- Данные проекта:");
                    Console.WriteLine($"Название проекта: {Engine.project.name}");
                    Console.WriteLine($"Размер сцены: X-{Engine.project.scene.X}, Y-{Engine.project.scene.Y}");
                    Console.WriteLine($"Положение obj: X-{Engine.project.scene.objX}, Y-{Engine.project.scene.objY}");
                    Console.WriteLine($"Кол-во строчек кода: {Engine.project.code.Count}");
                    Console.WriteLine($"Кол-во переменных: {Variables.var.Count}");
                    Console.WriteLine($"Кол-во объектов (fill): {Fill.fill.Count}");
                    Console.WriteLine($"Тип рендера: {Engine.project.scene.renderType}");
                    Text.Enter();
                    break;
                case ("/p.render" or "/render"):
                    switch (StrRead("Type"))
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
                            Text.Enter();
                            break;
                    }
                    break;
                case ("/p.scenesize" or "/scenesize"):
                    Engine.project.scene.X = IntRead("X");
                    Engine.project.scene.Y = IntRead("Y");
                    break;
                case ("/p.color" or "/color"):
                    strArg1 = StrRead("Target");
                    if (strArg1 == "obj")
                    {
                        Engine.project.scene.objColorFG = StrRead("ColorFG");
                        Engine.project.scene.objColorBG = StrRead("ColorBG");
                    }
                    else if (strArg1 == "field")
                    {
                        Engine.project.scene.fieldColorFG = StrRead("ColorFG");
                        Engine.project.scene.fieldColorBG = StrRead("ColorBG");
                    }
                    else
                    { Text.Error("Выбран неверный объект"); Text.Enter(); }
                    break;
                case ("/p.texture" or "/texture"):
                    strArg1 = StrRead("Target");
                    strArg2 = StrRead("Texture");
                    if (strArg1 == "obj")
                    { Engine.project.scene.objTexture = strArg2; }
                    else if (strArg1 == "fill")
                    { Engine.project.scene.fillTexture = strArg2; }
                    else if (strArg1 == "field")
                    { Engine.project.scene.fieldTexture = strArg2; }
                    else
                    { Text.Error("Указан несуществующий объект"); Text.Enter(); }
                    break;
                case ("/p.name" or "/name"):
                    Engine.project.name = StrRead("Новое имя проекта");
                    break;
                case ("/save" or "/s"):
                    Engine.project.Save();
                    break;
                case ("/help" or "/h"):
                    Project.Help();
                    break;
                case ("/help1" or "/h1" or "/helpCode"):
                    Project.HelpCode();
                    break;
                case ("/help2" or "/h2" or "/helpCommands"):
                    Project.HelpAllCommands();
                    break;
                case ("/help3" or "/h3" or "/helpEngineFunctions"):
                    Project.HelpEngineFunctions();
                    break;
                case ("/engine"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("- ConsoleEngine:");
                    Console.WriteLine($"by LazernikProjects");
                    Console.WriteLine($"Engine version: {Engine.version}");
                    Console.WriteLine($"CEL version: {Engine.versionCEL}");
                    Console.WriteLine($"Framework: {Engine.framework}");
                    Console.WriteLine($"Engine folder: C:\\ConsoleEngine");
                    Console.WriteLine($"GitHub: https://github.com/LazernikProjects/ConsoleEngine");
                    Text.Enter();
                    break;
                case ("-custom" or "custom" or "-c"):
                    repeatAnswer = StrRead("Name");
                    intArg1 = IntRead("IntArg1");
                    intArg2 = IntRead("IntArg2");
                    strArg1 = StrRead("StrArg1");
                    strArg2 = StrRead("StrArg2");
                    Engine.project.code.Add(new(repeatAnswer, "custom", intArg1, intArg2, strArg1, strArg2));
                    break;
                case ("-edit" or "edit" or "-e"):
                    intArg1 = IntRead("Line number"); intArg1--;
                    Engine.project.code[intArg1].IntArg1 = IntRead("IntArg1");
                    Engine.project.code[intArg1].IntArg2 = IntRead("IntArg2");
                    Engine.project.code[intArg1].StrArg1 = StrRead("StrArg1");
                    Engine.project.code[intArg1].StrArg2 = StrRead("StrArg2");
                    break;
                case ("-delete" or "delete" or "-d"):
                    intArg1 = IntRead("Line number");
                    Engine.project.code.RemoveAt(intArg1 - 1);
                    break;
                case ("-beta"):
                    Console.Write("\\Command [beta] = ");
                    strArg1 = Console.ReadLine();
                    if (strArg1 == "/package")
                    {
                        Packager.Package();
                    }
                    else
                    { Text.Error("Неверная команда"); Text.Enter(); }
                    break;
                case ("-editor"):
                    repeatAnswer = Console.ReadLine();
                    if (repeatAnswer == "codeHide")
                    { showCode = false; }
                    else if (repeatAnswer == "codeShow")
                    { showCode = true; }
                    break;
                default:
                    CodeWrite();
                    break;
            }
            Engine.project.scene.Render();
            CodeView();
            CodeWrite();
        }
        public static int IntRead(string text)
        {
            string outputPre;
            int output = 0;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\\{text} = ");
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                outputPre = Console.ReadLine();
                if (outputPre == "var")
                {
                    outputPre = StrRead("Variable name");
                    for (int i = 0; Variables.var.Count > i; i++)
                    {
                        if (Variables.var[i].Name == outputPre)
                        {
                            output = Variables.var[i].ID;
                            i = 99999;
                            return output;
                        }
                    }
                    Text.Error("Переменная с таким именем не найдена");
                    Text.Enter();
                }
                else
                {
                    output = int.Parse(outputPre);
                }
            }
            catch
            {
                output = 0;
                Text.Error("Введено неверное значение (Требуется число)");
                Text.Enter();
            }
            return output;
        }
        public static string StrRead(string text)
        {
            string output;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\\{text} = ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            output = Console.ReadLine();
            return output;
        }
        public static void CodeView()
        {
            if (showCode == false)
                return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#Code");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" - Для просмотра всех команд введите /help, для старта программы введите /start");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            for (int codeI = 0; codeI < Engine.project.code.Count; codeI++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{codeI + 1}.");
                commandType = Engine.project.code[codeI].Type;
                int intArg1 = Engine.project.code[codeI].IntArg1;
                int intArg2 = Engine.project.code[codeI].IntArg2;
                string strArg1 = Engine.project.code[codeI].StrArg1;
                string strArg2 = Engine.project.code[codeI].StrArg2;
                switch (Engine.project.code[codeI].Name)
                {
                    case ("pos"):
                        CodeV("pos", $"({intArg1}, {intArg2})", "", codeI);
                        break;
                    case ("moveX"):
                        CodeV("moveX", $"({intArg1})", "", codeI);
                        break;
                    case ("moveY"):
                        CodeV("moveY", $"({intArg1})", "", codeI);
                        break;
                    case ("fill"):
                        CodeV("fill", "", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                    case ("repeat"):
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"repeat({intArg1})");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($" \\{strArg1}");
                        Console.WriteLine();
                        Console.Write($" \\{strArg2}");
                        codeI += 2;
                        break;
                    case ("fillWithPos"):
                        CodeV("fillWithPos", $"({intArg1}, {intArg2})", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                    case ("wait"):
                        CodeV("wait", "", "", codeI);
                        break;
                    case ("texture"):
                        CodeV("texture", "", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                    case ("colorBG"):
                        CodeV("colorBG", "", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                    case ("colorFG"):
                        CodeV("colorFG", "", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                    case ("clear"):
                        CodeV("clear", "", "", codeI);
                        break;
                    case ("sceneSize"):
                        CodeV("sceneSize", $"({intArg1}, {intArg2})", "", codeI);
                        break;
                    case ("changeRender"):
                        CodeV("changeRender", "", $"[{strArg1}]", codeI);
                        break;
                    case ("nRepeat"):
                        CodeV("nRepeat", $"({intArg1})", "", codeI);
                        break;
                    case ("text"):
                        CodeV("text", "", $"[{strArg1}]", codeI);
                        break;
                    case ("var"):
                        CodeV("var", $"{intArg1}", $"{strArg1}", codeI);
                        break;
                    default:
                        CodeV($"{Engine.project.code[codeI].Name}", $"({intArg1}, {intArg2})", $"[{strArg1}, {strArg2}]", codeI);
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
        public static void CodeV(string name, string arg, string arg2, int index)
        {
            bool ifVar1 = false;
            string varName1 = "";
            string varName2 = "";
            /*if (Engine.project.code[index].IntArg1 > 15999 & Engine.project.code[index].IntArg1 < 17000)
            {
                ifVar1 = true;
                for (int i = 0; Variables.var.Count > i; i++)
                {
                    if (Engine.project.code[index].IntArg1 == Variables.var[i].ID)
                    {
                        varName1 = Variables.var[i].Name;
                        i = 9999;
                    }
                }
            }
            if (Engine.project.code[index].IntArg2 > 15999 & Engine.project.code[index].IntArg2 < 17000)
            {
                ifVar1 = true;
                for (int i = 0; Variables.var.Count > i; i++)
                {
                    if (Engine.project.code[index].IntArg2 == Variables.var[i].ID)
                    {
                        varName2 = Variables.var[i].Name;
                        i = 9999;
                    }
                }
            }
            if (ifVar1 == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (varName1 != "" & varName2 == "" & Engine.project.code[index].IntArg2 == 0)
                { Console.Write($"({varName1})"); }
                else if (varName1 == "" & varName2 != "" & Engine.project.code[index].IntArg1 == 0)
                { Console.Write($"({varName2})"); }
                else
                {
                    if (varName1 == "") { varName1 = Engine.project.code[index].IntArg1.ToString(); }
                    if (varName2 == "") { varName2 = Engine.project.code[index].IntArg2.ToString(); }
                    Console.Write($"({varName1}, {varName2})");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{arg2}");
            }*/
            if (commandType == "engine")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{arg}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{arg2}");
            }
            else if (commandType == "engine-repeat")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" \\{name}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"{arg}");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{arg2}");
            }
            else if (commandType == "custom")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{arg}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{arg2}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" - custom");
            }
            else if (commandType == "beta")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{arg}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{arg2}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" - beta");
            }
            else if (commandType == "engine-var")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($" {arg2}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" = ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{arg}");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{arg}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{arg2}");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" - unknown command");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        public static void CommandWrite(List<Operator> code, string name)
        {
            //string writtenCode = Console.ReadLine();
            switch (name.ToLower())
            {
                case "movex":
                    {
                        var codeN = new Move();
                        code.Add(codeN);
                        codeN.Input("moveX");
                        break;
                    }
                case "movey":
                    {
                        var codeN = new Move();
                        code.Add(codeN);
                        codeN.Input("moveY");
                        break;
                    }
                case "move":
                    {
                        var codeN = new Move();
                        code.Add(codeN);
                        codeN.Input("move");
                        break;
                    }
                case "pos":
                    {
                        var codeN = new Pos();
                        code.Add(codeN);
                        codeN.Input("");
                        break;
                    }
                case "fill":
                    {
                        var codeN = new FillOp();
                        code.Add(codeN);
                        codeN.Input("fill");
                        break;
                    }
                case "var":
                    {
                        var codeN = new Variable();
                        code.Add(codeN);
                        codeN.Input("");
                        break;
                    }
                case "repeat":
                    {
                        var codeN = new Repeat();
                        code.Add(codeN);
                        codeN.Input("");
                        break;
                    }
            }
        }
        public static OperatorValue ValueRead(string text)
        {
            string outputPre;
            int output = 0;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\\{text} = ");
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                outputPre = Console.ReadLine();
                if (outputPre == "var")
                {
                    outputPre = StrRead("Variable name");
                    for (int i = 0; Variables.var.Count > i; i++)
                    {
                        if (Variables.var[i].Name == outputPre)
                        {
                            return new VariableRef { VariableObj = Variables.var[i]};
                        }
                    }
                    Text.Error("Переменная с таким именем не найдена");
                    Text.Enter();
                }
                else
                {
                    output = int.Parse(outputPre);
                    return new OperatorValue { Value = output };
                }
            }
            catch
            {
                Text.Error("Введено неверное значение (Требуется число)");
                Text.Enter();
                
            }
            return new OperatorValue { Value = 0 };
        }
    }
}