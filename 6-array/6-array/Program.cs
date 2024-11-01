using System;

class Program
{
    public static void Main()
    {
        int n = 5;
        khayamPascal(n);
    }

    static void khayamPascal(int n)
    {
        int[,] triangle = new int[n, n];

        for (int line = 0; line < n; line++)
        {
            triangle[line, 0] = 1;
            triangle[line, line] = 1;

            for (int j = 1; j < line; j++)
            {
                triangle[line, j] = triangle[line - 1, j - 1] + triangle[line - 1, j];
            }
        }

        for (int line = 0; line < n; line++)
        {
            for (int space = 0; space < n - line ; space++)
            {
                Console.Write(" ");
            }

            for (int j = 0; j <= line; j++)
            {
                Console.Write(triangle[line, j] + " ");
            }

            Console.WriteLine();
        }
    }
}