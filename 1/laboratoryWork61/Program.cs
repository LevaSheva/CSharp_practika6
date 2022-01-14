using System;

namespace laboratoryWork61
{
      class Program
        {
            static int[] arr;
            static int size;

            static void Main(string[] args)
            {
                ShowMenu();
            }

        private static bool ShowMenu()
        {
            Console.WriteLine("\n\n:::::::::: Главное меню ::::::::::");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1. Инициализировать массив\n2. Распечатать массив\n3. Отсортировать по убыванию только отрицательные элементы массива");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("0. Выход из программы");
            Console.WriteLine(" ");
            if (arr == null)
            {
                Console.WriteLine("\nДля того, чтобы воспользоваться всеми возможностями программы, необходимо сгенерировать одномерный массив!\n ");
            }
            Console.Write("\nВведите номер действия: ");

            switch (Console.ReadLine())
            {
                case "1":
                    bool c = false;
                    while (!c)
                    {
                        size = ReadInt("\nВведите размер массива (n): ");
                        if (size >= 1)
                        {
                            arr = Array_Fill(size);
                            c = true;
                            ShowMenu();
                        }
                        else
                            Console.WriteLine("\nРазмер массива не может быть меньше 1! Пожалуйста, повторите попытку!\n");
                    }
                    ShowMenu();
                    return true;
                case "2":
                    if (arr != null)
                    {
                       Console.WriteLine("\nОдномерный массив:\n");
                       Array_Print(arr);
                    }
                    else
                        Console.WriteLine("\nМассив не инициализирован!\n");
                    ShowMenu();
                    return true;
                case "3":
                    if (arr != null)
                    {
                        Array_SortNeg(arr);
                        Console.WriteLine("\nРезультат сортировки:\n ");
                        Array_Print(arr);
                    }
                    else
                        Console.WriteLine("\nМассив не инициализирован!\n");
                    ShowMenu();
                    return true;
                case "0":
                    Environment.Exit(0);
                    return true;

                default:
                    Console.WriteLine("\nНекорректный ввод! Пожалуйста, повторите попытку!\n");
                    ShowMenu();
                    return true;
            }
        }

        private static void Array_Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"| {arr[i], 3} ");
            }
            Console.WriteLine("|");
        }

        private static int[] Array_Fill(int size)
        {
            int[] arr = new int[size];

            Console.WriteLine("\nВыберите способ формирования массива:\n1. Вручную\n2. С помощью класса Random");             
            
            switch (ReadInt("\nВведите номер действия: ", 1, 2))
                {
                    case 1:
                        for (int i = 0; i < size; i++)
                        {
                            arr[i] = ReadInt($"Введите значение элемента массива с индексом {i}:  ");
                        }
                        break;

                    case 2:
                        Random rnd = new Random();

                        for (int i = 0; i < size; i++)
                        {
                            arr[i] = rnd.Next(-50, 50);
                        }
                        break;
                }
                Console.WriteLine("\nМассив успешно инициализирован!\n");
                return arr;
        }

        // Отсортировать по убыванию только отрицательные элементы массива
        private static void Array_SortNeg(int[] arr)
        {
            int i, j, r;
            for (i = 0; i < arr.Length; i++)
            {
                for (j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] < 0 && arr[j] < 0)
                    {
                        if (arr[i] < arr[j])
                        {
                            r = arr[i];
                            arr[i] = arr[j];
                            arr[j] = r;
                        }
                    }
                }
            }
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
      }
}
