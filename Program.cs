using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/**<remark>
 * Write 10 numbers and 10 strings to the same file. Numbers are written from one thread, strings from another one.
 * Запишите 10 чисел и 10 строк в один и тот же файл. Числа записываются из одного потока, строки из другого.
 *
 * https://msdn.microsoft.com/ru-ru/library/system.threading.tasks.parallel.invoke(v=vs.110).aspx
</remark> */

namespace _20180329_Task3_Parallel_Invoke
{
    internal class Program
    {
        private static readonly StreamWriter commonResource = new StreamWriter(@"CommonFile.txt", false);

        private static void FirstTread()
        {
            Console.WriteLine("Первый поток начал запись!");

            for (int i = 1; i <= 10; i++)
            {
                commonResource.WriteLine(i);
                Thread.Sleep(500);
            }
            Console.WriteLine("Первый поток завершил запись!");
        }

        private static void SecondTread()
        {
            Console.WriteLine("Второй поток начал запись!");

            for (int i = 1; i <= 10; i++)
            {
                commonResource.WriteLine("Hello User!");
                Thread.Sleep(500);
            }
            Console.WriteLine("Второй поток завершил запись!");
        }

        private static void Main()
        {
            try
            {
                // Метод Parallel.Invoke() для параллельного выполнения двух методов
                Parallel.Invoke(FirstTread, SecondTread);

                commonResource.Close();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("An action has thrown an exception.\n{0}", e.InnerException?.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

            Console.ReadKey();
        }
    }
}