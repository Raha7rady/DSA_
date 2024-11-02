using System;

public class BigNumber
{
    private string digits;

    private BigNumber(string str)
    {
        digits = str.TrimStart('0');
        if (digits == "") digits = "0";
    }

    public BigNumber(int number = 0)
    {
        if (number == 0)
        {
            digits = "0";
        }
        else
        {
            digits = "";
            while (number > 0)
            {
                digits = (char)((number % 10) + '0') + digits;
                number /= 10;
            }
        }
    }

    public static BigNumber operator +(BigNumber a, BigNumber b)
    {
        string result = "";
        int carry = 0;
        int len1 = a.digits.Length;
        int len2 = b.digits.Length;
        int maxLen = Math.Max(len1, len2);
        for (int i = 0; i < maxLen || carry != 0; i++)
        {
            int digit1 = (i < len1) ? a.digits[len1 - 1 - i] - '0' : 0;
            int digit2 = (i < len2) ? b.digits[len2 - 1 - i] - '0' : 0;
            int sum = digit1 + digit2 + carry;
            carry = sum / 10;
            result = (char)((sum % 10) + '0') + result;
        }
        return new BigNumber(result);
    }

    public static BigNumber operator -(BigNumber a, BigNumber b)
    {
        string result = "";
        int borrow = 0;
        int len1 = a.digits.Length;
        int len2 = b.digits.Length;

        bool isNegative = a.CompareTo(b) < 0;

        if (isNegative)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        for (int i = 0; i < len1; i++)
        {
            int digit1 = a.digits[len1 - 1 - i] - '0';
            int digit2 = (i < len2) ? b.digits[len2 - 1 - i] - '0' : 0;
            int diff = digit1 - digit2 - borrow;
            if (diff < 0)
            {
                diff += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }
            result = (char)(diff + '0') + result;
        }

        int pos = 0;
        while (pos < result.Length && result[pos] == '0') pos++;
        if (pos == result.Length) return new BigNumber("0");
        return new BigNumber((isNegative ? "-" : "") + result.Substring(pos));
    }

    public int CompareTo(BigNumber other)
    {
        if (this.digits.Length != other.digits.Length)
        {
            return this.digits.Length.CompareTo(other.digits.Length);
        }
        for (int i = 0; i < this.digits.Length; i++)
        {
            if (this.digits[i] != other.digits[i])
            {
                return this.digits[i].CompareTo(other.digits[i]);
            }
        }
        return 0;
    }

    public static BigNumber operator <<(BigNumber a, int shift)
    {
        return new BigNumber(a.digits + new string('0', shift));
    }

    public static BigNumber operator >>(BigNumber a, int shift)
    {
        if (shift >= a.digits.Length) return new BigNumber(0);
        return new BigNumber(a.digits.Substring(0, a.digits.Length - shift));
    }

    public void Print()
    {
        Console.Write(digits);
    }

    public static void Main()
    {
        Console.Write("Enter the first number: ");
        int input1 = int.Parse(Console.ReadLine());
        Console.Write("Enter the second number: ");
        int input2 = int.Parse(Console.ReadLine());

        BigNumber num1 = new BigNumber(input1);
        BigNumber num2 = new BigNumber(input2);

        Console.Write("Enter number of positions to shift the first number to the left: ");
        int shiftLeftNum1 = int.Parse(Console.ReadLine());

        Console.Write("Enter number of positions to shift the first number to the right: ");
        int shiftRightNum1 = int.Parse(Console.ReadLine());

        Console.Write("Enter number of positions to shift the second number to the left: ");
        int shiftLeftNum2 = int.Parse(Console.ReadLine());

        Console.Write("Enter number of positions to shift the second number to the right: ");
        int shiftRightNum2 = int.Parse(Console.ReadLine());

        BigNumber shiftedNum1Left = num1 << shiftLeftNum1;
        BigNumber shiftedNum1Right = num1 >> shiftRightNum1;

        BigNumber shiftedNum2Left = num2 << shiftLeftNum2;
        BigNumber shiftedNum2Right = num2 >> shiftRightNum2;

        BigNumber sum = num1 + num2;
        BigNumber diff = num1 - num2;

        Console.Write("Sum: ");
        sum.Print();
        Console.WriteLine();

        Console.Write("Difference: ");
        diff.Print();
        Console.WriteLine();

        Console.Write("First Number Shifted Left: ");
        shiftedNum1Left.Print();
        Console.WriteLine();

        Console.Write("First Number Shifted Right: ");
        shiftedNum1Right.Print();
        Console.WriteLine();

        Console.Write("Second Number Shifted Left: ");
        shiftedNum2Left.Print();
        Console.WriteLine();

        Console.Write("Second Number Shifted Right: ");
        shiftedNum2Right.Print();
        Console.WriteLine();


    }
}
