using System;

class BigNumber
{
    private int[] digits;
    private int size;

    public BigNumber(int capacity)
    {
        digits = new int[capacity];
        size = 1;
        digits[0] = 1;
    }

    public void Multiply(int number)
    {
        int carry = 0;

        for (int i = 0; i < size; i++)
        {
            int product = digits[i] * number + carry;
            digits[i] = product % 10;
            carry = product / 10;
        }

        while (carry > 0)
        {
            digits[size] = carry % 10;
            carry /= 10;
            size++;
        }
    }

    public override string ToString()
    {
        char[] result = new char[size];
        for (int i = 0; i < size; i++)
        {
            result[size - 1 - i] = (char)(digits[i] + '0');
        }
        return new string(result);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter a number (1-100) to calculate its factorial: ");
        int n = int.Parse(Console.ReadLine());

        if (n < 1 || n > 100)
        {
            Console.WriteLine("Number must be between 1 and 100.");
            return;
        }

        BigNumber factorialResult = Fact(n);
        Console.WriteLine($"Factorial of {n} is: {factorialResult}");
    }

    static BigNumber Fact(int n)
    {
        BigNumber result = new BigNumber(500);

        for (int i = 2; i <= n; i++)
        {
            result.Multiply(i);
        }

        return result;
    }
}