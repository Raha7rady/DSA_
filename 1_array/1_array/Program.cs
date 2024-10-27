using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter the Fibonacci sequence number (n): ");
        int n = int.Parse(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine("The entered number must be non-negative.");
            return;
        }

        long[] fib = new long[n + 1];

        fib[0] = 0;
        if (n > 0)
        {
            fib[1] = 1;
        }

        for (int i = 2; i <= n; i++)
        {
            fib[i] = fib[i - 1] + fib[i - 2];
        }

        Console.WriteLine($"The {n}th sequence of the Fibonacci series: { fib[n]}");
    }
}