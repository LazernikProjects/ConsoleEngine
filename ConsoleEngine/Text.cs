using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Text
    {
        public static void Success(string successText)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (successText == "" || successText == null)
            { Console.WriteLine($"- Успешно!"); }
            else
            { 
                Console.Write($"- Успех: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{successText}");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(string warnText)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{{!}} Предупреждение: {warnText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{{!}} Ошибка: {errorText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void CriticalError(string criticalErrorText)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{{!}} Критическая ошибка: {criticalErrorText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Enter()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("- Press ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("'Enter'");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}