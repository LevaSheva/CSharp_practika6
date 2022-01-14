using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace laboratoryWork62
{
    class Program
    {
        static string str;

        static void Main(string[] args)
        {
            while (ShowMainMenu());
        }

        private static bool ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("::::::::::Главное меню::::::::::");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Ввести строку");
            Console.WriteLine("2. Распечатать строку");
            Console.WriteLine("3. Выполнить обработку");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("0. Выход");
            Console.WriteLine();

            switch (ReadInt("Выберите действие: ", 0, 3))
            {
                case 1:
                    {
                        EnteringString();
                        return true;
                    }
                case 2:
                    {
                        PrintString();
                        return true;
                    }
                case 3:
                    {
                        TaskAction();
                        return true;
                    }
                default: 
                    { 
                        break; 
                    }
            }

            return false;
        }
   
        private static void EnteringString()
        {
            bool s;
            s = false;
            while (!s)
            {
                Console.Write("\nВведите строку: ");
                str = Console.ReadLine();
                if (str.Length != 0 && string.IsNullOrWhiteSpace(str) != true) 
                    s = true;
                else if (str.Length == 0)
                    Console.WriteLine("\nВы ввели пустую строку! Повторите попытку!");
                else if (str.Length != 0 && string.IsNullOrWhiteSpace(str) == true)
                    Console.WriteLine("\nВы ввели строку, состояющую только из пробелов! Повторите попытку!");
            }
            Pause();
        }


        private static void PrintString(bool pause = true)
        {
            Console.Write("\nСтрока: ");
            Console.WriteLine(@"'{0}'", str);
            if (pause)
                Pause();
        }

 
        private static void TaskAction()
        {
            // Проверка, не является ли строка пустой и удаление пробельных символов
            if (str == null || str.Trim() == String.Empty) 
            { 
                Console.WriteLine("\nСтрока не инициализирована! Повторите попытку!");
                Pause();
                return; 
            }

            // Загрузка списка ключевых слов С# и их разбиение по строкам с удалением пустых строк
            string[] keywords = File.ReadAllText("CSharp_Keywords.txt")
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Разбивка строки по словам, т.к. символы и числа не нужны
            MatchCollection matches = Regex.Matches(str, @"\w+");
            var result = matches.Cast<Match>()
                .Select(m => m.Value) // Выборка только значения из массива (коллекции) совпадений, проецируем элементы
                .Where(keywords.Contains) // Выборка по условию, что текущий элемент имеется в файле (массиве keywords)
                .GroupBy(n => n) // Группировка  элементов
                .Select(g => new { Word = g.First(), Count = g.Count() }); // Проецируем для каждого совпадения объект с параметрами Word и Count
            
            if (result.Count() == 0) 
            {
                Console.WriteLine("\nВ введённой строке ключевые слова C# отсутствуют!");
                Pause();
                return;
            }

            PrintString(false);

            // перебор коллекции ключевых слов и вывод результов
            foreach (var e in result)
                Console.WriteLine($"\nКлючевое слово {e.Word} встречается {e.Count} раз(а)");
            Pause();
        }  

        private static int ReadInt(string str, int min = -1, int max = -1)
        {
            int size;
            bool b;
            do
            {
                Console.Write(str);
                b = int.TryParse(Console.ReadLine(), out size);
                if (!b)
                {
                    Console.WriteLine("\nНекорректный ввод! Пожалуйста, повторите попытку!\n");
                }
                if ((min != -1 && size < min) || (max != -1 && max < size))
                {
                    Console.WriteLine($"\nОшибка: введённое значение не входит в диапазон допустимых значений [{min};{max}]! Повторите попытку!\n");
                    b = false;
                }
            }
            while (b == false);

            return size;
        }

        private static void Pause()
        {
            Console.Write("\nДля продолжения нажмите клавишу Enter...");
            Console.ReadLine();
        }

    }
}
