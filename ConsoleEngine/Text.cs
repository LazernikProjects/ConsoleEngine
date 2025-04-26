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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"- Успешно: {successText}");
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
    }
}
