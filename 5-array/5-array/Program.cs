using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the size of the array (n): ");
        int n = int.Parse(Console.ReadLine());

        int[,] a = new int[n, n];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"a[{i}][{j}]: ");
                a[i, j] = int.Parse(Console.ReadLine());

            }
        }

        Console.WriteLine("Array:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(a[i, j] + "\t");
            }
            Console.WriteLine();
        }

        ChangeValueInt(a, n);

        Console.WriteLine("Modified array:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(a[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void ChangeValueInt(int[,] a, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    a[i, j]++;
                }

                if (i + j == n - 1)
                {
                    a[i, j]--;
                }

                if (i < j && i + j < n - 1)
                {
                    a[i, j] += 2;
                }

                if (i > j && i + j > n - 1)
                {
                    a[i, j] -= 2;
                }

                if (j < i && j + i < n - 1)
                {
                    a[i, j] += 3;
                }

                if (j > i && j + i > n - 1)
                {
                    a[i, j] -= 3;
                }
            }
        }
    }
}