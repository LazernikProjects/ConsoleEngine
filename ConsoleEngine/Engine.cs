using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    [Serializable]
    public class Engine
    {
        public static string version = "1.0.0";
        public static List<string> projects = new();
        public static Project project { get; set; } = null;

        public static void Loading()
        { 
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- ConsoleEngine");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" [Refactored] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"- Version: {version}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("- Loading...");
            try
            {
                string jsonString = File.ReadAllText("C:\\ConsoleEngine\\data.txt");
                Console.WriteLine("- read file data.txt...");
                projects = JsonSerializer.Deserialize<List<string>>(jsonString);
            }
            catch(Exception ex)
            {
                Error($"{ex}");
            }

            Console.WriteLine("- Success!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("- Press 'Enter'");
            Console.ReadLine();
            ProjectSelect();
        }
        public static void ProjectSelect()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Выберите проект - всего ({projects.Count}) [В разработке]");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Имя проекта: {projects[i]}");

                Console.ForegroundColor = ConsoleColor.Gray;
                // Make a reference to a directory.
                DirectoryInfo di = new DirectoryInfo($"C:\\ConsoleEngine\\Projects\\{projects[i]}");
                // Get a reference to each file in that directory.
                FileInfo[] fiArr = di.GetFiles();
                // Display the names and sizes of the files.
                foreach (FileInfo f in fiArr)
                    Console.WriteLine($"Размер файла: {f.Length} байт");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Путь: C:\\ConsoleEngine\\Projects\\{projects[i]}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine();
            }
            Console.WriteLine("Чтобы создать проект введите 'create', загрузить - 'load'");

            switch (Console.ReadLine())
            {
                case ("create" or "c"):
                    ProjectCreate();
                    break;
                case ("load" or "l"):
                    project = Project.Load();
                    Compiler.Start(project);
                    break;
                default:
                    Error("Invalid value");
                    Console.ReadLine();
                    ProjectSelect();
                    break;
            }
        }

        public static void ProjectCreate()
        {
            Console.Clear();
            Console.WriteLine("Создание проекта");
            Console.WriteLine();
            Console.WriteLine("Введите название проекта");
            project = new Project();
            project.name = Console.ReadLine();
            Console.WriteLine("Введите [X] сцены");
            try { project.scene.X = int.Parse(Console.ReadLine()); }
            catch (FormatException ex) 
            {
                Error($"Введено неверное значение - [Подробнее: {ex.Message}]");
                project.scene.X = 5;
            }
            Console.WriteLine("Введите [Y] сцены");
            try { project.scene.Y = int.Parse(Console.ReadLine()); }
            catch (FormatException ex)
            {
                Error($"Введено неверное значение - [Подробнее: {ex.Message}]");
                project.scene.Y = 5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Проект успешно создан!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

            project.scene.Render();
            Compiler.Start(project);
        }

        public static void Error(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {errorText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}