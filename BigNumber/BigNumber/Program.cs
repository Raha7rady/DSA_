using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

public class BigNumber
{
    private List<int> digits; 
    private bool isNegative;   

    public BigNumber()
    {
        digits = new List<int>();
        isNegative = false;
    }

    public BigNumber(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentException("Input cannot be null or empty.");

        isNegative = str[0] == '-';
        digits = new List<int>();

        for (int i = (isNegative ? 1 : 0); i < str.Length; i++)
        {
            if (!char.IsDigit(str[i]))
                throw new FormatException("Input string must contain only digits.");
            digits.Add(int.Parse(str[i].ToString()));
        }
    }

    public BigNumber(int number)
    {
        isNegative = number < 0;
        digits = new List<int>();

        number = Math.Abs(number);
        do
        {
            digits.Insert(0, number % 10);
            number /= 10;
        } while (number > 0);
    }

    public BigNumber Add(BigNumber other)
    {
        int comparison = Compare(this, other);

        if (isNegative == other.isNegative)
        {
            var resultDigits = AddDigits(digits, other.digits);
            return new BigNumber((isNegative ? "-" : "") + string.Join("", resultDigits));
        }
        else
        {
            if (comparison > 0)
            {
                var resultDigits = SubtractDigits(digits, other.digits);
                return new BigNumber((isNegative ? "" : "-") + string.Join("", resultDigits));
            }
            else if (comparison < 0)
            {
                var resultDigits = SubtractDigits(other.digits, digits);
                return new BigNumber((other.isNegative ? "" : "-") + string.Join("", resultDigits));
            }
            else
            {
                return new BigNumber("0");
            }
        }
    }

    public BigNumber Subtract(BigNumber other)
    {
        if (isNegative != other.isNegative)
        {
            return Add(other.Negate());
        }
        else
        {
            bool resultNegative = Compare(this, other) < 0;
            var resultDigits = SubtractDigits(resultNegative ? other.digits : digits, resultNegative ? digits : other.digits);
            return new BigNumber((resultNegative ? "-" : "") + string.Join("", resultDigits));
        }
    }

    public BigNumber Negate()
    {
        return new BigNumber(string.Join("", digits)) { isNegative = !isNegative };
    }

    private List<int> AddDigits(List<int> num1, List<int> num2)
    {
        var result = new List<int>();
        int carry = 0;
        int maxLength = Math.Max(num1.Count, num2.Count);

        for (int i = 0; i < maxLength; i++)
        {
            int digit1 = i < num1.Count ? num1[num1.Count - 1 - i] : 0;
            int digit2 = i < num2.Count ? num2[num2.Count - 1 - i] : 0;
            int sum = digit1 + digit2 + carry;
            result.Add(sum % 10);
            carry = sum / 10;
        }

        if (carry > 0)
            result.Add(carry);

        result.Reverse();
        return result;
    }

    private List<int> SubtractDigits(List<int> minuend, List<int> subtrahend)
    {
        var result = new List<int>();
        int borrow = 0;

        for (int i = 0; i < minuend.Count; i++)
        {
            int digit1 = minuend[minuend.Count - 1 - i];
            int digit2 = (i < subtrahend.Count ? subtrahend[subtrahend.Count - 1 - i] : 0) + borrow;

            if (digit1 < digit2)
            {
                digit1 += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }

            result.Add(digit1 - digit2);
        }

        result.Reverse();

        while (result.Count > 1 && result[0] == 0)
            result.RemoveAt(0);

        return result;
    }

    private int Compare(BigNumber a, BigNumber b)
    {
        if (a.isNegative && !b.isNegative) return -1;
        if (!a.isNegative && b.isNegative) return 1;

        if (a.digits.Count != b.digits.Count)
            return a.digits.Count > b.digits.Count ? 1 : -1;

        for (int i = 0; i < a.digits.Count; i++)
        {
            if (a.digits[i] != b.digits[i])
                return a.digits[i] > b.digits[i] ? 1 : -1;
        }

        return 0; 
    }

    public override string ToString()
    {
        return (isNegative ? "-" : "") + string.Join("", digits);
    }

    public BigNumber ShiftLeft(int positions)
    {
        if (positions < 0)
            throw new ArgumentException("Positions must be non-negative.");

        var shiftedDigits = new List<int>(digits);
        for (int i = 0; i < positions; i++)
        {
            shiftedDigits.Add(0);
        }

        return new BigNumber((isNegative ? "-" : "") + string.Join("", shiftedDigits));
    }

    public BigNumber ShiftRight(int positions)
    {
        if (positions < 0)
            throw new ArgumentException("Positions must be non-negative.");

        if (positions >= digits.Count)
        {
            return new BigNumber("0");
        }

        var shiftedDigits = digits.GetRange(0, digits.Count - positions);
        return new BigNumber((isNegative ? "-" : "") + string.Join("", shiftedDigits));
    }

    

    public BigNumber Divide(BigNumber divisor)
    {
        if (divisor.IsZero())
            throw new DivideByZeroException("Cannot divide by zero.");

        bool resultIsNegative = this.isNegative != divisor.isNegative;

        BigNumber absDividend = this.isNegative ? this.Negate() : this;
        BigNumber absDivisor = divisor.isNegative ? divisor.Negate() : divisor;

        int diffSize = absDividend.digits.Count - absDivisor.digits.Count;
        BigNumber scaledDivisor = new BigNumber(absDivisor.ToString() + new string('0', diffSize));
        BigNumber currentDividend = new BigNumber(string.Join("", absDividend.digits));
        BigNumber quotient = new BigNumber();

        while (diffSize >= 0)
        {
            int counter = 0;

            while (Compare(currentDividend, scaledDivisor) >= 0)
            {
                currentDividend = currentDividend.Subtract(scaledDivisor);
                counter++;
            }

            if (counter > 0)
                quotient = quotient.Add(new BigNumber(counter * (int)Math.Pow(10, diffSize)));

            diffSize--;
            if (diffSize >= 0)
                scaledDivisor = new BigNumber(absDivisor.ToString() + new string('0', diffSize));
        }

        if (quotient.IsZero())
            quotient.isNegative = false; 
        else
            quotient.isNegative = resultIsNegative;

        return quotient;
    }



    private BigNumber NegateIfNegative()
    {
        return isNegative ? this.Negate() : this;
    }

    private bool IsZero()
    {
        return digits.Count == 1 && digits[0] == 0;
    }



    public BigNumber MultiplyKaratsuba(BigNumber other)
    {
        bool resultNegative = isNegative != other.isNegative;

        var resultDigits = Karatsuba(this, other).digits;

        if (resultDigits == null || resultDigits.Count == 0)
        {
            throw new InvalidOperationException("Result digits cannot be empty.");
        }

        var result = new BigNumber(string.Join("", resultDigits));
        result.isNegative = resultNegative;

        return result;
    }


    private BigNumber Karatsuba(BigNumber x, BigNumber y)
    {
        if (x.ToString() == "0" || y.ToString() == "0")
            return new BigNumber("0");

        int maxLength = Math.Max(x.digits.Count, y.digits.Count);

        if (maxLength < 2)
        {
            return new BigNumber((x.Multiply(y)).ToString());
        }

        int halfLength = (maxLength + 1) / 2;
        var a = x.digits.Take(x.digits.Count - halfLength);
        var b = y.digits.Take(y.digits.Count - halfLength);
        var c = new BigNumber("0");

        var xHigh = new BigNumber(string.Join("", a.Count() > 0 ? a : c.digits.Take(1)));
        var xLow = new BigNumber(string.Join("", x.digits.Skip(x.digits.Count - halfLength)));
        var yHigh = new BigNumber(string.Join("", b.Count() > 0 ? b : c.digits.Take(1)));
        var yLow = new BigNumber(string.Join("", y.digits.Skip(y.digits.Count - halfLength)));

        var z0 = Karatsuba(xLow, yLow);
        var z1 = Karatsuba(xLow.Add(xHigh), yLow.Add(yHigh));
        var z2 = Karatsuba(xHigh, yHigh);

        var result = z2.Shift(2 * halfLength).Add(z1.Subtract(z2).Subtract(z0).Shift(halfLength)).Add(z0);

        return RemoveLeadingZeros(result);
    }

    private BigNumber Shift(int count)
    {
        var shiftedDigits = new List<int>(digits);
        for (int i = 0; i < count; i++)
            shiftedDigits.Add(0);

        return new BigNumber(string.Join("", shiftedDigits));
    }


    private BigNumber RemoveLeadingZeros(BigNumber number)
    {
        while (number.digits.Count > 1 && number.digits[0] == 0)
        {
            number.digits.RemoveAt(0);
        }

        return number;
    }


    public BigNumber Power(int exponent)
    {
        if (exponent < 0)
            throw new ArgumentException("Exponent must be non-negative.");

        if (exponent == 0)
            return new BigNumber(1); 

        if (exponent == 1)
            return this; 

        int halfExponent = exponent / 2;
        BigNumber halfPower = this.Power(halfExponent);

        if (exponent % 2 == 0)
        {
            // Even exponent: (a^b)^2
            return halfPower.Multiply(halfPower);
        }
        else
        {
            // Odd exponent: a^b * a^(b-1)
            return halfPower.Multiply(halfPower).Multiply(this);
        }
    }

    public BigNumber Multiply(BigNumber other)
    {
        if (this.digits.Count == 0 || other.digits.Count == 0)
            return new BigNumber("0");

        bool resultIsNegative = this.isNegative != other.isNegative;

        int[] resultDigits = new int[this.digits.Count + other.digits.Count];

        for (int i = this.digits.Count - 1; i >= 0; i--)
        {
            for (int j = other.digits.Count - 1; j >= 0; j--)
            {
                int product = this.digits[i] * other.digits[j];
                int tempSum = product + resultDigits[i + j + 1]; 

                resultDigits[i + j + 1] = tempSum % 10;
                resultDigits[i + j] += tempSum / 10;
            }
        }

        var finalResult = new List<int>();
        foreach (var digit in resultDigits)
        {
            if (!(finalResult.Count == 0 && digit == 0)) 
                finalResult.Add(digit);
        }

        if (finalResult.Count == 0)
            finalResult.Add(0);

        BigNumber result = new BigNumber(string.Join("", finalResult));
        result.isNegative = resultIsNegative;

        return result;
    }

    public BigNumber Factorial()
    {
        if (isNegative)
        {
            throw new ArgumentException("Factorial is not defined for negative numbers.");
        }

        if (digits.Count == 1 && digits[0] == 0)
        {
            return new BigNumber(1);
        }

        BigNumber result = new BigNumber(1);
        BigNumber current = new BigNumber(1);

        while (Compare(current, this) <= 0)
        {
            result = result.Multiply(current);
            current = current.Add(new BigNumber(1));
        }

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
            try
            {
                Console.WriteLine("Enter the first number:");
                string input1 = Console.ReadLine();
                BigNumber num1 = new BigNumber(input1);

                Console.WriteLine("Enter the second number:");
                string input2 = Console.ReadLine();
                BigNumber num2 = new BigNumber(input2);


                Console.WriteLine($"Sum: {num1.Add(num2)}");

                Console.WriteLine($"Difference: {num1.Subtract(num2)}");

                Console.WriteLine($"Multiplication: {num1.Multiply(num2)}");

                try
                {         
                    Console.WriteLine($"Multiplication(karatsuba): {num1.MultiplyKaratsuba(num2)}");

                    Console.WriteLine($"Division: {num1.Divide(num2)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during division: {ex.Message}");
                }

                Console.WriteLine("Enter another number for shift, power and factorial operations:");
                string input3 = Console.ReadLine();
                BigNumber num3 = new BigNumber(input3);

            Console.Write("Enter the number of bits to shift left: ");
            int leftShift = int.Parse(Console.ReadLine());
            Console.WriteLine($"Shift Left by {leftShift}: {num3.ShiftLeft(leftShift)}");

            Console.Write("Enter the number of bits to shift right: ");
            int rightShift = int.Parse(Console.ReadLine());
            Console.WriteLine($"Shift Right by {rightShift}: {num3.ShiftRight(rightShift)}");

            Console.Write("Enter an exponent for power operation: ");
                int exponent = int.Parse(Console.ReadLine());
                Console.WriteLine($"{num3} ^ {exponent} = {num3.Power(exponent)}");


            try
                {
                    Console.WriteLine($"Factorial of {num3} is {num3.Factorial()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating factorial: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
