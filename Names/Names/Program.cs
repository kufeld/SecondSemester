using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Names
{
    class Program
    {
        static void Main()
        {
            var names = new List<string>();

            Console.WriteLine("Обрабатываю данные");

            try
            {
                using (StreamReader sr = new StreamReader("names.txt", Encoding.GetEncoding(1251)))
                {
                    string line;
                    while((line = sr.ReadLine())!= null)
                    {
                       if (!names.Contains(line))
                        {
                            Console.Write('.');
                            names.Add(line);
                        }
                    }
                }
            }
            catch { }

            names.Sort();


            Console.WriteLine();
            Console.WriteLine("Обработка данных завершена");
            Console.WriteLine($"Имен списке: {result.Count}");

            try
            {
                using(StreamWriter sr = new StreamWriter("result.txt",false, Encoding.Default))
                {
                    foreach (var e in result)
                        sr.WriteLine(Char.ToUpper(e[0]) + e.Substring(1, e.Length-1));
                }
            }
            catch { }

            Console.ReadLine();
        }
    }
}
