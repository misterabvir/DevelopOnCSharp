namespace HM6.FractionalExceptions;

public class InputStringDoesNotMatchTheExpectedFormatException : Exception
{
    public InputStringDoesNotMatchTheExpectedFormatException() : base(message: "Input string does not match the expected format \r\nPlease enter a valid input string")
    {        
    }
}