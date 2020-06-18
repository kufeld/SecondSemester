using System;
using System.Collections.Generic;
using System.IO;

namespace Birthdays
{
    class Program
    {
        public static Dictionary<String, List<string>> dictionary = new Dictionary<string, List<string>>();
        static void Main()
        {
            var month = new int[12];

            try
            {
                using(StreamReader sr = new StreamReader("birthday.txt"))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        ParseLineString(line);
                    }
                }
            }
            catch {Console.WriteLine("Нет данных"); }

            while (true)
            {
                Console.WriteLine("Введите имя");
                var name = Console.ReadLine();
                if (name == "") break;
                if(!dictionary.ContainsKey(name))
                {
                    Console.WriteLine("Данного имени нет в списке");
                    continue;
                }
                month = CountBirthdays(name);
                WriteDataInConsole(month);
                Console.ReadLine();
            }
        }

        private static void WriteDataInConsole(int[] month)
        {
            Console.WriteLine();
            for (int i = 0; i < month.Length; i++)
                Console.Write($"{i + 1}         ");
            Console.WriteLine();
            for(int i = 0; i < month.Length; i++)
                Console.Write($"{month[i]}         ");
        }

        private static int[] CountBirthdays(string name)
        {
            var result = new int[12];
            var count = dictionary[name];
            
            foreach(var e in count)
            {
                var month = e.Split('.')[1];
                result[int.Parse(month) - 1]++;
            }

            return result;
        }

        private static void ParseLineString(string line)
        {
            var key = line.Split(' ')[0];
            var value = line.Split(' ')[1];
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, new List<string>());
            dictionary[key].Add(value);
        }
    }
}
