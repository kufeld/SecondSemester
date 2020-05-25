using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCipher
{
    public class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Введите текст");
                var line = Console.ReadLine();
                if (line == "") break;
                line = LineProcessing(line);
                var cipherLine = Cipher(line);
                Thread.Sleep(200);
                Console.WriteLine("Зашифровали:");
                Console.WriteLine(cipherLine);
                var decipherLine = Decipher(cipherLine);
                Thread.Sleep(200);
                Console.WriteLine("Расшифровали:");
                Console.WriteLine(decipherLine);
            }
            Console.ReadLine();
        }


        public static string Decipher(string line)
        {
            var result = new StringBuilder();
            var stack = new Stack<char>();

            foreach(var e in line)
            {
                if (stack.Count == 0) 
                    stack.Push(e);
                else if (stack.Peek() != e)
                    stack.Push(e);
                else
                    stack.Pop();
            }

            var endLine = stack.Reverse();
            foreach (var e in endLine)
                result.Append(e);

            return result.ToString();
        }
        public static string Cipher(string line)
        {
            var rnd = new Random();
            var count = rnd.Next(line.Length, 3 * line.Length);
            var result = new StringBuilder();
            
            var number = rnd.Next(0, line.Length / 2);
            count -= number;
 
            for (int i = 0; i <number; i++)
            {
                var ch = (char)rnd.Next('a', 'z');
                for (int j = 0; j < 2; j++)
                    result.Append(ch);
            }

            foreach(var e in line)
            {
                if (count == 0) break;
                if (rnd.Next(-10, 10) > 0)
                {
                    count--;
                    var ch = (char)rnd.Next('a', 'z');
                    for (int j = 0; j < 2; j++)
                        result.Append(ch);
                }
                result.Append(e);
            }

            if(count != 0)
                for(int i = 0; i < count; i++)
                {
                    var ch = (char)rnd.Next('a', 'z');
                    for (int j = 0; j < 2; j++)
                        result.Append(ch);
                }
                
            return result.ToString();
        }

        public static string LineProcessing(string line)
        {
            line = line.ToLower();
            var resulte = new StringBuilder();
            var currentChar = line[0];
            resulte.Append(currentChar);
            for(var i =1; i < line.Length; i++)
            {
                var e = line[i];
                if (Char.IsLetter(e))
                {
                    if (e != currentChar)
                        resulte.Append(e);
                    currentChar = e;
                }
            }
            return resulte.ToString();
        }
    }
}
