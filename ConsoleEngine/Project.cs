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

        public string name; //{ get; set; }
        public List<string> variables = new List<string>();
        public List<int> variablesID = new List<int>();
        public void Save()
        {
            try
            {
                scene.ProjectSaveName = name;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Сохранение проекта...");
                if (Directory.Exists($"C:\\ConsoleEngine\\Projects\\{name}") == true)
                { Console.WriteLine("- project folder found"); }
                else
                {
                    Console.WriteLine("- project folder not found");
                    Console.WriteLine("- create project folder...");
                    Directory.CreateDirectory($"C:\\ConsoleEngine\\Projects\\{name}");
                }
                if (Engine.projects.Contains(name))
                { Console.WriteLine("- data.txt already contains this project"); }
                else
                {
                    Console.WriteLine("- write file data.txt...");
                    Engine.projects.Add(name);
                    string dataSave = JsonSerializer.Serialize(Engine.projects);
                    File.WriteAllText("C:\\ConsoleEngine\\data.txt", dataSave);
                }
                Console.WriteLine("- write file project.ceproj...");
                string projectSave = JsonSerializer.Serialize(this);
                File.WriteAllText($"C:\\ConsoleEngine\\Projects\\{name}\\project.ceproj", projectSave);
                Text.Success("Проект сохранен!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"- Path: C:\\ConsoleEngine\\Projects\\{name}\\project.ceproj");
                Text.Enter();
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); }
        }
        public static Project Load()
        {
            string jsonString = null;
            try
            {
                Console.WriteLine("Укажите путь к файлу (project.ceproj)");
                string path = Console.ReadLine();
                jsonString = File.ReadAllText(path);
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); }
            Console.WriteLine("read file project.ceproj...");
            var project = JsonSerializer.Deserialize<Project>(jsonString);
            Text.Success("Проект загружен!");
            Text.Enter();
            return project;
        }
        public static Project LoadSelectProject()
        {
            string jsonString = null;
            try
            {
                string path = $"C:\\ConsoleEngine\\Projects\\{Engine.selectedProject}\\project.ceproj";
                jsonString = File.ReadAllText(path);
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); }
            Console.WriteLine("read file project.ceproj...");
            var project = JsonSerializer.Deserialize<Project>(jsonString);
            Text.Success("Проект загружен!");
            Text.Enter();
            return project;
        }
        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("- Документация по CEL и движку");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- Для просмотра введите следующие команды:");
            HelpCommand2("/help1", "Все возможности CEL (Код)");
            HelpCommand2("/help2", "Все команды");
            HelpCommand2("/help3", "Функции движка");
            Text.Enter();
            //Engine.project.scene.Render();
            //Compiler.Start(Engine.project);
        }
        public static void HelpCode()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Help - все команды (При написании кода напишите ТОЛЬКО НАЗВАНИЕ КОМАНДЫ, без аргументов, а аргументы напишите в соответствующем окне)");
            Console.WriteLine("#Code");
            HelpCommand("repeat", "(value)", "", "Цикл, повторяет две команды указанное кол-во раз");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" \\command1");
            Console.WriteLine(" \\command2");
            HelpCommand("nRepeat", "(value)", "", "Более современный цикл, повторяет любые команды неограниченное кол-во раз");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" \\command1");
            Console.WriteLine(" \\command2");
            HelpCommand("pos", "(x, y)", "", "Перемещает obj в указанную позицию");
            HelpCommand("move", "(x, y)", "", "Изменяет позицию obj на указанное значение {X}&{Y}");
            HelpCommand("moveX", "(x)", "", "Изменяет {X} позицию obj на указанное значение");
            HelpCommand("moveY", "(y)", "", "Изменяет {Y} позицию obj на указанное значение");
            HelpCommand("fill", "", "[colorFG, colorBG]", "Создает объект (fill) на месте obj с которым нельзя взаимодействовать");
            HelpCommand("fillD", "", "", "Создает 'default' объект (fill) на месте obj с которым нельзя взаимодействовать");
            HelpCommand("fillWithPos", "(x, y)", "[colorFG, colorBG]", "Создает объект (fill) в указанных координатах с которым нельзя взаимодействовать");
            HelpCommand("fillWithPosD", "(x, y)", "", "Создает 'default' объект (fill) в указанных координатах с предустановленным цветом");
            HelpCommand("clear", "", "", "Удаляет объект (fill) на месте obj");
            HelpCommand("wait", "", "", "Требует нажатия 'enter' после рендера сцены");
            HelpCommand("text", "", "[text]", "Выводит текст");
            HelpCommand("texture", "", "[targer, texture]", "Изменяет текстуру у указанного объекта (obj, fill, field)");
            HelpCommand("colorFG", "", "[targer, color]", "Изменяет ForegroundColor у указанного объекта (obj, fill, field)");
            HelpCommand("colorBG", "", "[targer, color]", "Изменяет BackgroundColor у указанного объекта (obj, fill, field)");
            HelpCommand("changeRender", "", "[renderType]", "Изменяет тип рендера (default, wait, fast)");
            HelpCommand("sceneSize", "(x, y)", "", "Изменяет размер сцены");
            Text.Enter();
        }
        public static void HelpAllCommands()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("#Commands");
            HelpCommand2("/start", "Старт выполнения программы");
            HelpCommand2("/info", "Debug информация программы");
            HelpCommand2("/save", "Сохраняет проект в файл");
            HelpCommand2("/render", "Изменяет тип рендера (default, wait, fast)");
            HelpCommand2("/sceneSize", "Изменяет размер сцены");
            HelpCommand2("/texture", "Изменяет текстуру у указанного объекта (obj, fill, field)");
            HelpCommand2("/color", "Изменяет цвет у указанного объекта (obj, fill, field)");
            HelpCommand2("/name", "Изменяет имя проекта");
            HelpCommand2("/engine", "Выводит информацию о движке");
            Text.Enter();
        }
        public static void HelpEngineFunctions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("#EngineFunctions");
            HelpCommand3("-edit", "Позволяет изменять код");
            HelpCommand3("-delete", "Позволяет удалять код");
            HelpCommand3("-custom", "Позволяет создавать кастомные команды");
            HelpCommand3("-beta", "Позволяет использовать beta-команды");
            Text.Enter();
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