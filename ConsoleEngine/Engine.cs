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
        public static string version = "2.0-beta1";
        public static string versionCEL = "3.0"; //Версия ConsoleEngineLanguage
        public static string packagerVersion = "0.2 [Beta]";
        public static string framework = "self-contained";

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
            Console.WriteLine($"- Выберите проект - всего ({projects.Count})");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Название: {projects[i]}");
                Console.ForegroundColor = ConsoleColor.Gray;
                DirectoryInfo di = new DirectoryInfo($"C:\\ConsoleEngine\\Projects\\{projects[i]}");
                FileInfo[] fiArr = di.GetFiles();
                foreach (FileInfo f in fiArr)
                    Console.WriteLine($"Размер: {f.Length} байт");
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
            project = new Project();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("- Создание проекта");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Название: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            project.name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Размер сцены {X}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            try { project.scene.X = int.Parse(Console.ReadLine()); }
            catch (FormatException)
            { Text.Error("Введите число! (Установлено дефолтное значение {5})"); project.scene.X = 5; }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Размер сцены {Y}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            try { project.scene.Y = int.Parse(Console.ReadLine()); }
            catch (FormatException)
            { Text.Error("Введите число! (Установлено дефолтное значение {5})"); project.scene.Y = 5; }
            /*Console.WriteLine("Показать дополнительные настройки? (yes/no)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "y" || answer == "да")
            {
                Console.WriteLine("- Дополнительные настройки:");
                Console.Write($"renderType={project.scene.renderType}");
            }*/
            Text.Success("Проект создан!");
            Text.Enter();
            project.scene.Render();
            Compiler.Start(project);
        }
    }
}