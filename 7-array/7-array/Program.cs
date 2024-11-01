using System;

class Program
{
    static void Main()
    {
        int[,] a =
            {
                   { 1, 2, 3 },
                   { 4, 5, 6 }
             };

        int[,] b =
            {
                   { 1, 4 },
                   { 2, 5 },
                   { 3, 6 }
             };

        int[,] c =
        {
            { 1, 4 },
            { 0, 0 },
            { 3, 6 }
        };

        bool result1 = isTranspose(a, b, 2);
        bool result2 = isTranspose(a, c, 2);
        Console.WriteLine("Are they transposes? " + result1);
        Console.WriteLine("Are they transposes? " + result2);
    }

    static bool isTranspose(int[,] a, int[,] b, int n)
    {
        int rowsA = a.GetLength(0);
        int colsA = a.GetLength(1);
        int rowsB = b.GetLength(0);
        int colsB = b.GetLength(1);

        if (rowsA != colsB || colsA != rowsB)
        {
            return false;
        }

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsA; j++)
            {
                if (a[i, j] != b[j, i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}