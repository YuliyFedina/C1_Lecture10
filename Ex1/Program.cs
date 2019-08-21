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

            const string path = @"..\..\..\SomeDir";
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists) dirInfo.Create();

            const string filePath = path + @"\fileForLecture10.txt";
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) fileInfo.Create();

            Console.WriteLine("Вводите числа через enter (если хотите завершить, введите \"-1\"): ");

            using (var sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                int value;
                while ((value = Helper.InputInt()) != -1)
                    sw.WriteLine(value);
            }

            try
            {
                using (var sr = new StreamReader(filePath))
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