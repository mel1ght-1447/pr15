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
        static void WriteToFile(string filepath, uint n)
        {
            try
            {
                FileStream F1 = new FileStream(filepath, FileMode.Create);
                StreamWriter writer = new StreamWriter(F1, Encoding.Default);
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
            }
            catch (IOException ioEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка открытия файла для записи: " + ioEx.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (FormatException fEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка : " + fEx.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static int[] ReadFromFile(string filepath)
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
                    tempstring = tempstring.Trim();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка открытия файла для записи: " + ioEx.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            catch (FormatException fEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка : " + fEx.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }
        static void CalculateAndDisplay(int[] array)
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Массив пуст");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static uint InputNumber()
        {            
                uint n = 0;
            try
            {
                Console.Write("Введите количество чисел: ");
                n = Convert.ToUInt32(Console.ReadLine());
            }
            catch (OverflowException oFe)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка : " + oFe.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
            }
            catch (FormatException fEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка : " + fEx.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка : " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
            }
                return n;                     
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
                Console.Clear();
                Console.WriteLine("Здравствуйте!");
                Console.WriteLine("Введите путь к файлу и имя файла \n Например: .\\pr15.txt");
                filepath = Console.ReadLine();
                if (filepath == null || filepath.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не указан путь к файлу");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
                uint n = InputNumber();
                WriteToFile(filepath, n);
                array = ReadFromFile(filepath);
                CalculateAndDisplay(array);
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
        }
    }
}

