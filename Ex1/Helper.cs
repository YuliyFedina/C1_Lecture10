using System;

namespace Ex1
{
    public class Helper
    {
        public static int InputInt(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var inputInt))
                {
                    if (inputInt <= max && inputInt >= min)
                        return inputInt;
                    Console.Write($"Введеное значение должно быть в диапазоне {min}-{max}: ");
                    continue;
                }

                Console.WriteLine("Введено некорректное значение! Попробуйте еще раз ввести число (int)");
            }
        }
    }
}