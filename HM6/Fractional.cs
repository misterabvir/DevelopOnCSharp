using HM6.FractionalExceptions;
using System.Text.RegularExpressions;

namespace HM6;

public partial class Fractional
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    private Fractional(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
        Reduce();
        NegativeCorrection();
    }

    #region implicit operators
    public static implicit operator Fractional(string input) => Parse(input);

    public static implicit operator Fractional(double value)
    {
        string doubleAsString = value.ToString();
        int decimalPlaces = doubleAsString.Length - doubleAsString.IndexOf('.') - 1;
        int denominator = (int)Math.Pow(10, decimalPlaces);
        int numerator = (int)(value * denominator);
        return new Fractional(numerator, denominator);
    }
    #endregion

    #region parsers
    public static Fractional Parse(string input)
    {
        if (double.TryParse(input, out double result))
            return result;

        Regex regex = FractionalRegex();
        Match match = regex.Match(input);

        if (match.Success)
        {
            string[] digits = match.Groups[0].Value.Split('/');

            int firstNumber = int.Parse(digits[0]);
            int secondNumber = int.Parse(digits[1]);

            if (secondNumber == 0)
                throw new DenominatorCannotBeZeroException();

            return new Fractional(firstNumber, secondNumber);
        }

        throw new InputStringDoesNotMatchTheExpectedFormatException();
    }
    public static bool TryParse(string input, out Fractional? output)
    {
        output = null;
        if (double.TryParse(input, out double result))
        {
            output = result;
            return true;
        }


        Regex regex = FractionalRegex();
        Match match = regex.Match(input);

        if (match.Success)
        {
            string[] digits = match.Groups[0].Value.Split('/');

            int firstNumber = int.Parse(digits[0]);
            int secondNumber = int.Parse(digits[1]);

            if (secondNumber == 0)
                return false;

            output = new(firstNumber, secondNumber);
            return true;
        }

        return false;
    }
    #endregion

    #region math operators
    public static Fractional operator +(Fractional left, Fractional right)
    {
        int commonDenominator = left.Denominator * right.Denominator;
        int adjustedLeftNumerator = left.Numerator * right.Denominator;
        int adjustedRightNumerator = right.Numerator * left.Denominator;
        int resultNumerator = adjustedLeftNumerator + adjustedRightNumerator;
        return new Fractional(resultNumerator, commonDenominator);
    }

    public static Fractional operator -(Fractional left, Fractional right)
    {
        int commonDenominator = left.Denominator * right.Denominator;
        int adjustedLeftNumerator = left.Numerator * right.Denominator;
        int adjustedRightNumerator = right.Numerator * left.Denominator;
        int resultNumerator = adjustedLeftNumerator - adjustedRightNumerator;
        return new Fractional(resultNumerator, commonDenominator);
    }

    public static Fractional operator *(Fractional left, Fractional right)
    {
        return new(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
    }

    public static Fractional operator /(Fractional left, Fractional right)
    {
        return new(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
    }

    #endregion

    #region privates
    private void Reduce()
    {

        int gcd = FindGCD(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    private void NegativeCorrection()
    {
        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
    }

    private int FindGCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    [GeneratedRegex(@"^-?\d+/-?\d+")]
    private static partial Regex FractionalRegex();
    #endregion

    public override string ToString() => Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";
}