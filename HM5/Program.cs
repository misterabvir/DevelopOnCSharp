using HM5.App;
using System.Threading.Channels;

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
/* OUTPUT
Enter 'q' or leave blank to exit, 'c' to undo last change
12
> 12
/
5
> 2.4
*
-3
> -7.2
+
0.8
> -6.4
-
-0.4
> -6
/
0
divide by zero
> -6
incorrect
invalid input
c
> -6
c
> -6.4
c
> -7.2
c
> 2.4
c
> 12
c
not found previous value
q
*/