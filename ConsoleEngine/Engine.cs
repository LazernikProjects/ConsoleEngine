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
        public static string version = "1.2.0-beta4";
        public static string versionCEL = "3.0-beta3"; //Версия ConsoleEngineLanguage
        public static string packagerVersion = "0.2 [Beta]";
        public static string framework = ".NET 9.0";

        public static string selectedProject = null;
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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine($"- by LazernikProjects");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("- Loading...");
            try
            {
                string jsonString = File.ReadAllText("C:\\ConsoleEngine\\data.txt");
                Console.WriteLine("- Read file data.txt...");
                projects = JsonSerializer.Deserialize<List<string>>(jsonString);
            }
            catch
            { Console.WriteLine("- Engine directory not found"); }
            Console.WriteLine("- Success!");
            Text.Enter();
            ProjectSelect();
        }
        public static void ProjectSelect()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Выберите проект - всего ({projects.Count})");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Имя проекта: {projects[i]}");

                Console.ForegroundColor = ConsoleColor.Gray;
                DirectoryInfo di = new DirectoryInfo($"C:\\ConsoleEngine\\Projects\\{projects[i]}");
                FileInfo[] fiArr = di.GetFiles();
                foreach (FileInfo f in fiArr)
                    Console.WriteLine($"Размер файла: {f.Length} байт");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Путь: C:\\ConsoleEngine\\Projects\\{projects[i]}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine();
            }
            Console.WriteLine("Чтобы создать проект введите 'create', загрузить - 'load'");

            selectedProject = Console.ReadLine();
            if (projects.Contains(selectedProject))
            {
                project = Project.LoadSelectProject();
                project.name = project.scene.ProjectSaveName;
                project.scene.ProjectSaveName = null;
                Compiler.Start(project);
            }
            else
            {
                switch (selectedProject)
                {
                    case ("create" or "c"):
                        ProjectCreate();
                        break;
                    case ("load" or "l"):
                        project = Project.Load();
                        project.name = project.scene.ProjectSaveName;
                        project.scene.ProjectSaveName = null;
                        Compiler.Start(project);
                        break;
                    default:
                        Text.Error("Invalid value");
                        Text.Enter();
                        ProjectSelect();
                        break;
                }
            }
        }
        public static void ProjectCreate()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- Создание проекта");
            Console.WriteLine();
            Console.WriteLine("Введите название проекта");
            project = new Project();
            project.name = Console.ReadLine();
            Console.WriteLine("Введите [X] сцены");
            try { project.scene.X = int.Parse(Console.ReadLine()); }
            catch (FormatException ex) 
            {
                Text.Error($"Введено неверное значение - [Подробнее: {ex.Message}]");
                project.scene.X = 5;
            }
            Console.WriteLine("Введите [Y] сцены");
            try { project.scene.Y = int.Parse(Console.ReadLine()); }
            catch (FormatException ex)
            {
                Text.Error($"Введено неверное значение - [Подробнее: {ex.Message}]");
                project.scene.Y = 5;
            }
            Text.Success("Проект создан!");
            Text.Enter();
            project.scene.Render();
            Compiler.Start(project);
        }
    }
}