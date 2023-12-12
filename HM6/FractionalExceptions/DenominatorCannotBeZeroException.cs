namespace HM6.FractionalExceptions;

public class DenominatorCannotBeZeroException : Exception
{
    public DenominatorCannotBeZeroException() : base(message: "Denominator cannot be zero \r\nPlease enter a valid denominator")
    {        
    }
}
