using System.Diagnostics;

namespace lab4
{
    public class lab4
    {
        const int k = 9;
        const int size = 20 + (int)(0.6 * k);

        static Stopwatch sw = new Stopwatch();

        static void Main()
        {
            int[] original = new int[size];
            int[] sorted;
            int search, count;

            Console.WriteLine("\t---=== Сортування та пошук у масиві ===---\n\n");

            Random rnd = new Random();
            for (int i = 0; i < original.Length; i++)
                original[i] = rnd.Next(10, 100);

            Console.WriteLine("Вихідний масив даних:\n");
            Console.WriteLine("{0}\n\n", string.Join(" ", original));

            sw.Start(); sorted = sortInsert(original); sw.Stop();

            Console.WriteLine($"\tСортування вставками (час: {sw.Elapsed}):\n");
            Console.WriteLine("{0}\n\n", string.Join(" ", sorted));


            sw.Start(); sorted = sortBuble(original); sw.Stop();

            Console.WriteLine($"\tСортування бульбашкою (час: {sw.Elapsed}):\n");
            Console.WriteLine("{0}\n\n", string.Join(" ", sorted));


            sw.Start(); sorted = sortIntegrated(original); sw.Stop();

            Console.WriteLine($"\tВбудоване сортування Array.Sort (час: {sw.Elapsed}):\n");
            Console.WriteLine("{0}\n\n", string.Join(" ", sorted));

            while (true)
            {
                Console.Write("Введіть число для пошуку: ");
                string? str = Console.ReadLine();
                if (int.TryParse(str, out search))
                    break;
                else
                    Console.WriteLine("Помилка! Введіть дійсне ціле число!");
            }
            Console.WriteLine();

            sw.Start();
            int leftIndex = searchBinary(sorted, search, true);
            int rightIndex = searchBinary(sorted, search);
            sw.Stop();
            count = (rightIndex - leftIndex) + 1;

            if (leftIndex < 0 || rightIndex < 0)
                Console.WriteLine($"Число {search} не знайдено в масиві (час: {sw.Elapsed})");
            else
                Console.WriteLine($"Бінарний пошук числа {search} (час: {sw.Elapsed}): зустрічається {count} разів.");

            sw.Start();
            count = searchLinear(sorted, search);
            sw.Stop();

            if (count == 0)
                Console.WriteLine($"Число {search} не знайдено в масиві (час: {sw.Elapsed})");
            else
                Console.WriteLine($"Лінійний пошук числа {search} (час: {sw.Elapsed}): зустрічається {count} разів.");

        }

        static int[] sortInsert(int[] source)
        {
            int[] result = new int[size];
            Array.Copy(source, result, size);

            for (int i = 1; i < size; i++)
                for (int j = i; j > 0 && result[j - 1] > result[j]; j--)
                    swapArrayElements(ref result[j - 1], ref result[j]);

            return result;
        }

        static int[] sortBuble(int[] source)
        {
            int[] result = new int[size];
            Array.Copy(source, result, size);

            for (int i = 1; i < size; i++)
                for (int j = 0; j < size - i; j++)
                    if (result[j] > result[j + 1])
                        swapArrayElements(ref result[j], ref result[j + 1]);

            return result;
        }

        static int[] sortIntegrated(int[] source)
        {
            int[] result = new int[source.Length];
            Array.Copy(source, result, result.Length);

            Array.Sort(result);

            return result;
        }

        static void swapArrayElements(ref int first, ref int second)
        {
            int tmp = first;
            first = second;
            second = tmp;
        }

        static int searchBinary(int[] arr, int search, bool first = false)
        {
            int start = 0;
            int end = size;
            int result = -1;

            while (start <= end)
            {
                int middle = (start + end) / 2;
                int midVal = arr[middle];

                if (midVal == search)
                {
                    result = middle;
                    if (first)
                        end = middle - 1;
                    else
                        start = middle + 1;
                }
                else if (midVal > search)
                    end = middle - 1;
                else
                    start = middle + 1;
            }

            return result;
        }

        static int searchLinear(int[] arr, int search)
        {
            int count = 0;

            for (int i = 0; i < size; i++)
                if (arr[i] == search)
                    count++;

            return count;
        }
    }
}