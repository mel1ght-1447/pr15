//*******************************************
//* Практическая работа №15                 *
//* Выполнил: Быков М.С., группа 2ИСП       *
//* Вариант: 2                              *
//*******************************************
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr15
{
    internal class Program
    {
        private static bool WriteToFile(string filepath, uint n)
        {
            try
            {
                FileStream F1 = new FileStream(filepath, FileMode.Create);
                StreamWriter writer = new StreamWriter(F1);
                Random rnd = new Random();
                int a;
                Console.WriteLine("---Генерация чисел---");
                for (int i = 0; i < n; i++)
                {
                    a = rnd.Next(-42, 17);
                    writer.Write(a + " ");
                }
                writer.Close();
                Console.WriteLine("Запись завершена");
                return true;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("Ошибка открытия файла для записи: " + ioEx.Message);
                return false;
            }
            catch (FormatException fEx)
            {
                Console.WriteLine("Ошибка : " + fEx.Message);
                return false;
            }
        }
        private static int[] ReadFromFile(string filepath)
        {
            try
            {
                Console.WriteLine($"Содержимое файла {filepath}: ");
                FileStream F2 = new FileStream(filepath, FileMode.Open);
                StreamReader reader = new StreamReader(F2, Encoding.Default);
                string tempstring;
                int[] array = null;
                while ((tempstring = reader.ReadLine()) != null)
                {
                    tempstring = tempstring.Trim(' ');
                    string[] text = tempstring.Split(' ');
                    array = new int[text.Length];
                    for (int i = 0; i < text.Length; i++)
                    {
                        array[i] = Convert.ToInt32(text[i]);
                    }
                }
                reader.Close();
                return array;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("Ошибка открытия файла для записи: " + ioEx.Message);
                return null;
            }
            catch (FormatException fEx)
            {
                Console.WriteLine("Ошибка : " + fEx.Message);
                return null;
            }
        }
            private static void CalculateAndDisplay(int[] array)
            {
                if (array != null && array.Length > 0)
                {
                    int min = array[0];
                    int max = array[0];

                    foreach (int currentElement in array)
                    {
                        Console.WriteLine($"\t{currentElement}");
                        if (currentElement > max)
                            max = currentElement;
                        if (currentElement < min)
                            min = currentElement;
                    }
                    int range = max - min;
                    Console.WriteLine($"Минимальный элемент: {min}");
                    Console.WriteLine($"Максимальный элемент: {max}");
                    Console.WriteLine($"Размах: {range}");
                }
                else
                {
                    Console.WriteLine("Массив пуст");
                }
            }
            static uint InputNumber()
            {
                while (true)
                {
                    Console.Write("Введите количество чисел: ");
                    uint n = Convert.ToUInt32(Console.ReadLine());
                    if (n > 0)
                    {
                        return n;
                    }
                    else
                        Console.WriteLine("Ошибка: введите положительное число");
                }
            }
            static void Main(string[] args)
            {
                string filepath;
                int[] array = null;
                bool repeat = true;
                Console.Title = "Практическая работа №15";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                while (repeat)
                {
                    Console.WriteLine("Здравствуйте!");
                    Console.WriteLine("Введите путь к файлу и имя файла, \n Например: .\\pr15.txt");
                    filepath = Console.ReadLine();
                    uint n = InputNumber();
                    bool writeSuccess = WriteToFile(filepath, n);
                    if (writeSuccess)
                    {
                        array = ReadFromFile(filepath);
                        CalculateAndDisplay(array);
                    }
                    Console.Write("Нажмите Y для выхода или N для повтора: ");
                    string selectKey = Console.ReadLine();
                    switch (selectKey)
                    {
                        case "Y":
                            repeat = false;
                            Environment.Exit(0);
                            break;
                        case "N":
                            Console.Clear();
                            repeat = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Введена неправильная буква");
                            Console.ForegroundColor = ConsoleColor.White;
                            Environment.Exit(0);
                            repeat = false;
                            break;
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
