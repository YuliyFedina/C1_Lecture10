using System;
using System.IO;
using System.Text;

namespace Ex1
{
    internal class Program
    {
        private static void Main()
        {
                //Ввести с консоли N чисел, записать их в файл. Считать числа из файла,
                //вывести на консоль. Сделать любым способом.

                string path = @"D:\SomeDir";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                const string fileForLecture10 = @"D:\SomeDir\fileForLecture10.txt";
                FileInfo fileInfo = new FileInfo(fileForLecture10);
                if (!fileInfo.Exists)
                {
                    fileInfo.Create();
                }

                
                using (new StreamWriter(fileForLecture10, false, Encoding.Default))
                {
                }

                Console.WriteLine("Вводите числа через enter (если хотите завершить, введите \"-1\"): ");

                int value;
                while ((value = Helper.InputInt()) != -1)
                    using (var sw = new StreamWriter(fileForLecture10, true, Encoding.Default))
                    {
                        sw.WriteLine(value);
                    }

                try
                {
                    using (var sr = new StreamReader(fileForLecture10))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }
    }
}