using HM6;
using HM6.App;
using HM6.FractionalExceptions;

//double implicit
Fractional fractional = 0.6;
Console.WriteLine(fractional); //"3/5"

//string implicit
fractional = "7/-8";
Console.WriteLine(fractional); //"-7/8"

//reduce
fractional = "20/40";
Console.WriteLine(fractional); //"1/2"

//simplyfier when denominator equal 1
fractional = "4/2";
Console.WriteLine(fractional); //"2"

/**************************************/

fractional = 0.5;

//add
Console.WriteLine(fractional + 2.5); //"3"

//substract
Console.WriteLine(fractional - "5/2"); //"-2"

//divide
Console.WriteLine(fractional / "4"); //"1/8"

//multiply
Console.WriteLine(fractional * "2/3"); //"1/3"


/**************************************/
//parse exceptions
try
{
    Fractional.Parse("1/0");
}
catch (DenominatorCannotBeZeroException exc)
{
    Console.Error.WriteLine(exc.Message); // "Denominator cannot be zero \r\n Please enter a valid denominator"
}

try
{
    Fractional.Parse("incorrect");
}
catch (InputStringDoesNotMatchTheExpectedFormatException exc)
{
    Console.Error.WriteLine(exc.Message); // "Input string does not match the expected format \r\n Please enter a valid input string")
}

/**************************************/
//Calculator Demo
ICalculator calc = new Calculator();
calc.OperandChangedEvent += (sender, e) => Print($"> {e.Operand}", ConsoleColor.Green);
calc.InvalidOperationEvent += (sender, e) => Print(e.Message, ConsoleColor.Red);

string input = string.Empty;
Console.WriteLine("Enter 'q' or leave blank to exit, 'c' to undo last change");

while (true)
{
    input = Console.ReadLine() ?? string.Empty;
    if (input.ToLower() == "q" || string.IsNullOrWhiteSpace(input))
    {
        return;
    }
    else if (input.ToLower() == "c")
    {
        calc.CancelLast();
    }
    else
        calc.Set(input);
}

static void Print(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine($"{message}");
    Console.ResetColor();
}

/*
 4
> 4
/
8/3
> 3/2
*
0.5
> 3/4
+
1/2
> 5/4
-
0.1
> 23/20
*
-5/4
> -23/16
/
1/-9
> 207/16
q
 */