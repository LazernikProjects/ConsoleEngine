using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleEngine
{
    [Serializable]
    public class Engine
    {
        public static string version = "2.0";
        public static string versionCEL = "4.0"; //Версия ConsoleEngineLanguage
        public static string packagerVersion = "0.2-beta";
        public static string framework = ".NET 8.0";
        public static int versionNumber = 2000;
        public static int minimalVersion = 2000;
        public static int minimalAvailableVersion = 1200;

        public static string EnginePath = "C:\\ConsoleEngine";
        public static string ProjectTypeSave = "default";
        public static string selectedProject = null;
        public static List<string> projects = new();
        public static Project project { get; set; } = null;
        public static void Loading()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- ConsoleEngine");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" [Refactored V2] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"- Version: {version}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine($"- by LazernikProjects");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("- Loading...");
            try
            {
                string jsonString = File.ReadAllText($"{EnginePath}\\data.txt");
                Console.WriteLine("- Read file data.txt...");
                projects = JsonSerializer.Deserialize<List<string>>(jsonString);
            }
            catch
            { Console.WriteLine("- Engine directory not found"); }
            try
            {
                string jsonString = File.ReadAllText($"{EnginePath}\\settings.txt");
                Console.WriteLine("- Read file settings.txt...");
                SettingsSave save = JsonSerializer.Deserialize<SettingsSave>(jsonString);
                Editor.showCode = save.ShowCode;
                Editor.showScene = save.ShowScene;
                EnginePath = save.Path;
                ProjectTypeSave = save.ProjectType;
            }
            catch
            { Console.WriteLine("- File settings.txt not found"); }
            Console.WriteLine("- Success!");
            Text.Enter();
            ProjectSelect();
        }
        public static void ProjectSelect()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"- Проекты [ConsoleEngine {version}]");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Название: {projects[i]}");
                Console.ForegroundColor = ConsoleColor.Gray;
                DirectoryInfo di = new DirectoryInfo($"{EnginePath}\\Projects\\{projects[i]}");
                FileInfo[] fiArr = di.GetFiles();
                foreach (FileInfo f in fiArr)
                    Console.WriteLine($"Размер: {f.Length} байт");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Путь: {EnginePath}\\Projects\\{projects[i]}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("----------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Команды:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("- Создать проект - 'create'");
            Console.WriteLine("- Загрузить проект по указонному пути - 'load'");
            Console.WriteLine("- Загрузить проект по названию - 'название проекта'");
            Console.WriteLine("- Удалить проект - 'delete'");
            Console.WriteLine("- Настройки движка - 'settings'");
            Console.ForegroundColor = ConsoleColor.Yellow;
            selectedProject = Console.ReadLine();
            if (projects.Contains(selectedProject))
            {
                project = Project.LoadSelectProject();
                Compiler.Start(project);
            }
            else
            {
                switch (selectedProject)
                {
                    case ("create" or "c"):
                        ProjectCreate();
                        break;
                    case ("delete" or "d"):
                        DeleteProject();
                        break;
                    case ("load" or "l"):
                        project = Project.Load();
                        Compiler.Start(project);
                        break;
                    case ("load-new" or "l"):
                        project = Project.Load();
                        project.Run(project.scene);
                        break;
                    case ("settings" or "s"):
                        Settings();
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
            Console.ForegroundColor = ConsoleColor.White;
            Text.Success("Проект создан!");
            Text.Enter();
            project.saveVersion = versionNumber;
            project.compilerType = ProjectTypeSave;
            if (project.compilerType == "default" || project.compilerType == "old")
            {
                project.scene.Render();
                Compiler.Start(project);
            }
            else if (project.compilerType == "new")
            {
                project.scene.Render();
                project.Run(project.scene);
            }
        }
        public static string CheckVersion(int version1)
        {
            if (version1 > versionNumber)
            {
                Text.Warning("Версия проекта новее версии движка! Это может привести к ошибкам");
                return "new";
            }
            else if (version1 == versionNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("- проект использует новейшую версию!");
                return "latest";
            }
            else if (version1 < versionNumber & version1 > minimalVersion)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("- проект использует поддерживаемую версию!");
                return "support";
            }
            else if (version1 < minimalVersion & version1 > minimalAvailableVersion)
            {
                Text.Warning("Проект использует старую версию! Это может привести к ошибкам");
                return "old";
            }
            else if (version1 < minimalAvailableVersion)
            {
                Text.Error("Проект использует слишком старую версию! Проект будет вести себя неккоректно");
                return "notsupport";
            }
            Text.Error("Неизвестная версия");
            return "unknown";
        }
        public static void DeleteProject()
        {
            try
            {
                Console.WriteLine("Укажите название проекта");
                string pName = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("- удаление project.ceproj...");
                File.Delete($"{EnginePath}\\Projects\\{pName}\\project.ceproj");
                Console.WriteLine("- удаление папки проекта...");
                Directory.Delete($"{EnginePath}\\Projects\\{pName}");
                Console.WriteLine("- удаление проекта из data.txt...");
                projects.Remove($"{pName}");
                string dataSave = JsonSerializer.Serialize(projects);
                File.WriteAllText($"{EnginePath}\\data.txt", dataSave);
                Text.Success($"Проект '{pName}' удален!");
                Text.Enter();
                ProjectSelect();
            }
            catch (Exception ex)
            {
                Text.CriticalError($"{ex}");
            }
        }
        public static void Settings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("- Меню настроек");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Version: {version} ({versionNumber})");
            Console.WriteLine($"CEL version: {versionCEL}");
            Console.WriteLine($"Framework: {framework}");
            Console.WriteLine($"GitHub: https://github.com/LazernikProjects/ConsoleEngine");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("1.Показывать код: "); Settings2(Editor.showCode);
            Console.Write("2.Показывать сцену: "); Settings2(Editor.showScene);
            Console.Write($"3.Путь к движку: {EnginePath} [BETA]"); Console.WriteLine();
            Console.Write($"4.Тип проекта: {ProjectTypeSave} [НЕ ИСПОЛЬЗУЙТЕ ЭТО]"); Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Выйти - 'exit', сохранить настройки - 'save'");
            Console.ForegroundColor = ConsoleColor.Yellow;
            switch (Console.ReadLine())
            {
                case "1" or "showCode":
                    if (Editor.showCode) { Editor.showCode = false; }
                    else { Editor.showCode = true; }
                    Settings();
                    break;
                case "2" or "showScene":
                    if (Editor.showScene) { Editor.showScene = false; }
                    else { Editor.showScene = true; }
                    Settings();
                    break;
                case "3" or "EnginePath":
                    Console.Write("New path: ");
                    EnginePath = Console.ReadLine();
                    Settings();
                    break;
                case "4" or "Type":
                    if (ProjectTypeSave == "default") { ProjectTypeSave = "new"; }
                    else { ProjectTypeSave = "default"; }
                    Settings();
                    break;
                case "s" or "save":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("- сохранение");
                    SettingsSave save = new SettingsSave(Editor.showCode, Editor.showScene, EnginePath, ProjectTypeSave);
                    string saveSettings = JsonSerializer.Serialize(save);
                    File.WriteAllText($"{EnginePath}\\settings.txt", saveSettings);
                    Text.Success("");
                    Text.Enter();
                    Settings();
                    break;
                case "e" or "exit":
                    ProjectSelect();
                    break;
                default:
                    Text.Error("Invalid value");
                    Text.Enter();
                    Settings();
                    break;
            }
        }
        public static void Settings2(bool value)
        {
            if (value == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Да");
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Нет");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}