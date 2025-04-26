using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    [Serializable]
    public class Project
    {
        public Scene scene { get; set; } = new Scene();
        public List<Code> code { get; set; } = new List<Code>();

        public string name; 
        public void Save()
        {
            try
            {
                //Engine.projects.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Сохранение проекта...");
                if (Directory.Exists($"C:\\ConsoleEngine\\Projects\\{name}") == true)
                {
                    Console.WriteLine("Папка проекта обнаружена");
                }
                else
                {
                    Console.WriteLine("Папка проекта не найдена");
                    Console.WriteLine("Создание папки...");
                    Directory.CreateDirectory($"C:\\ConsoleEngine\\Projects\\{name}");
                }
                Console.WriteLine();

                Console.WriteLine("write file data.txt...");
                Engine.projects.Add(name);
                string dataSave = JsonSerializer.Serialize(Engine.projects);
                File.WriteAllText("C:\\ConsoleEngine\\data.txt", dataSave);
                Console.WriteLine("write file project.txt...");
                string projectSave = JsonSerializer.Serialize(this);
                File.WriteAllText($"C:\\ConsoleEngine\\Projects\\{name}\\project.txt", projectSave);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.WriteLine($"Путь к файлу проекта - C:\\ConsoleEngine\\Projects\\{name}\\project.txt");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Text.CriticalError($"{ex}");
            }
        }

        public static Project Load()
        {
            Console.WriteLine("Укажите путь к файлу (project.txt)");
            string path = Console.ReadLine();
            string jsonString = File.ReadAllText(path);
            Console.WriteLine("read file project.txt...");
            var project = JsonSerializer.Deserialize<Project>(jsonString);

            Text.Success("Проект загружен!");
            Console.ReadLine();

            project.scene.Render();
            return project;
        }

        public static void Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Help - все команды (При написании кода напишите ТОЛЬКО НАЗВАНИЕ КОМАНДЫ, без аргументов, а аргументы напишите в соответствующем окне)");
            Console.WriteLine();
            Console.WriteLine("#Code");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("repeat");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("(value)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" - Цикл, повторяет две команды указанное кол-во раз");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine(" \\command1");
            Console.WriteLine(" \\command2");
            HelpCommand("pos", "(x, y)", "", "Перемещает Obj в указанную позицию");
            HelpCommand("moveX", "(x)", "", "Изменяет {X} позицию obj на указанное значение");
            HelpCommand("moveY", "(y)", "", "Изменяет {Y} позицию obj на указанное значение");
            HelpCommand("fill", "", "[colorFG, colorBG]", "Создает объект (fill) на месте obj с которым нельзя взаимодействовать");
            HelpCommand("fillD", "", "", "Создает 'default' объект (fill) на месте obj с которым нельзя взаимодействовать");
            HelpCommand("fillWithPos", "(x, y)", "[colorFG, colorBG]", "Создает объект (fill) в указанных координатах с которым нельзя взаимодействовать");
            HelpCommand("clear", "", "", "Удаляет объект (fill) на месте obj");
            HelpCommand("wait", "", "", "Требует нажатия 'enter' после рендера сцены");
            HelpCommand("texture", "", "[targer, texture]", "Изменяет текстуру у указанного объекта (obj, fill, field)");
            HelpCommand("colorFG", "", "[targer, color]", "Изменяет ForegroundColor у указанного объекта (obj, fill, field)");
            HelpCommand("colorBG", "", "[targer, color]", "Изменяет BackgroundColor у указанного объекта (obj, fill, field)");
            HelpCommand("changeRender", "", "[renderType]", "Изменяет тип рендера (default, wait, fast)");
            HelpCommand("sceneSize", "(x, y)", "", "Изменяет размер сцены");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("#Commands");
            HelpCommand2("/start", "Старт выполнения программы");
            HelpCommand2("/info", "Debug информация программы");
            HelpCommand2("/save", "Сохраняет проект в файл");
            HelpCommand2("/p.render", "Изменяет тип рендера (default, wait, fast)");
            HelpCommand2("/p.sceneSize", "Изменяет размер сцены");
            HelpCommand2("/p.texture", "Изменяет текстуру у указанного объекта (obj, fill, field)");
            HelpCommand2("/p.color", "Изменяет Color у указанного объекта (obj, fill, field)");
            Console.ReadLine();
            Engine.project.scene.Render();
            Compiler.Start(Engine.project);
        }

        public static void HelpCommand(string name, string arg, string arg2, string description) //HelpCommand("", "", "", ""); 
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{arg}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{arg2}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" - {description}");
            Console.WriteLine();
        }
        public static void HelpCommand2(string name, string description) //Только для команд, не кода 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" - {description}");
            Console.WriteLine();
        }
        public static void HelpCommand3(string name, string description) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" - {description}");
            Console.WriteLine();
        }
    }
}
