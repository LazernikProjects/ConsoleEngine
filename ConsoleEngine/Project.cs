using ConsoleEngine.Operators;
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
        public List<Operator> Operators { get; set; } = new List<Operator>();

        public string? name { get; set; }
        public int saveVersion { get; set; } = 0;
        public string compilerType { get; set; } = "default";
        public void Run(Scene scene)
        {
            Variables.var.Clear();
            foreach (Operator op in Operators)
            {
                op.Run(scene);
            }
            Display();
            while (true)
            {
                string name = Editor.StrRead("Name");
                if (name == "/start")
                {
                    break;
                }
                if (name == "/save")
                {
                    Engine.project.Save();
                }
                else { Editor.CommandWrite(Engine.project.Operators, name); }
            }
            Run(scene);
        }
        internal void Display()
        {
            foreach (Operator op in Operators)
            {
                op.Display(0);
            }
        }
        public void Save()
        {
            try
            {
                saveVersion = Engine.versionNumber;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Сохранение проекта...");
                if (Directory.Exists($"{Engine.EnginePath}\\Projects\\{name}") == true)
                { Console.WriteLine("- project folder found"); }
                else
                {
                    Console.WriteLine("- project folder not found");
                    Console.WriteLine("- create project folder...");
                    Directory.CreateDirectory($"{Engine.EnginePath}\\Projects\\{name}");
                }
                if (Engine.projects.Contains(name))
                { Console.WriteLine("- data.txt already contains this project"); }
                else
                {
                    Console.WriteLine("- write file data.txt...");
                    Engine.projects.Add(name);
                    string dataSave = JsonSerializer.Serialize(Engine.projects);
                    File.WriteAllText($"{Engine.EnginePath}\\data.txt", dataSave);
                }
                Console.WriteLine("- write file project.ceproj...");
                string projectSave = JsonSerializer.Serialize(this);
                File.WriteAllText($"{Engine.EnginePath}\\Projects\\{name}\\project.ceproj", projectSave);
                Text.Success("Проект сохранен!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"- Path: {Engine.EnginePath}\\Projects\\{name}\\project.ceproj");
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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Путь к файлу проекта: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string path = Console.ReadLine();
                jsonString = File.ReadAllText(path);
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- read file project.ceproj...");
            var project = JsonSerializer.Deserialize<Project>(jsonString);
            Engine.CheckVersion(project.saveVersion);
            Text.Success("Проект загружен!");
            Text.Enter();
            return project;
        }
        public static Project LoadSelectProject()
        {
            string jsonString = null;
            try
            {
                string path = $"{Engine.EnginePath}\\Projects\\{Engine.selectedProject}\\project.ceproj";
                jsonString = File.ReadAllText(path);
            }
            catch (Exception ex)
            { Text.CriticalError($"{ex}"); }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- read file project.ceproj...");
            var project = JsonSerializer.Deserialize<Project>(jsonString);
            Engine.CheckVersion(project.saveVersion);
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
            HelpCommand2("/help4", "Переменные (не закончено!!!)");
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
            HelpCommand2("/version", "Проверяет версию проекта");
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
            HelpCommand3("-settings", "Быстрые настройки");
            Text.Enter();
        }
        public static void HelpVariables()
        {
            Console.Clear();
            Text.Warning("Документация по переменным не закончена!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("#Variables");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- Создать переменную (для создания напишите 'var')");
            HelpCommand4("var", "имя", "значение");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- Изменение переменной (просто напишите имя переменной)");
            Console.WriteLine("- Также вы можете подсунуть значение переменно для команды требующей числовое значение, написав сначала 'var', вместо значения, а затем имя переменной [Пример: moveX(имя переменной)]");
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
        public static void HelpCommand4(string name, string arg, string arg2) //HelpCommand("", "", "", ""); 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($" {arg}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{arg2}");
            Console.WriteLine();
        }
    }
}