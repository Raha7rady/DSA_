using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter the value of n: ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Please enter the value of r: ");
        int r = int.Parse(Console.ReadLine());

        if (r > n || r < 0)
        {
            Console.WriteLine("The value of r must be between 0 and n.");
            return;
        }

        long result = Combination(n, r);
        Console.WriteLine($"C({n}, {r}) = {result}");
    }

    static long Combination(int n, int r)
    {
        if (r > n || r < 0)
            return 0;

        if (r > n - r)
            r = n - r;

        long[] numerator = new long[r];
        long[] denominator = new long[r];

        for (int i = 0; i < r; i++)
        {
            numerator[i] = n - i; 
            denominator[i] = i + 1; 
        }

        long result = 1;
        for (int i = 0; i < r; i++)
        {
            result *= numerator[i];
            result /= denominator[i];
        }

        return result;
    }
}
