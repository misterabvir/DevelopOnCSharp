namespace HM6.App;

public class OperandChangedEventArgs(Fractional operand) : EventArgs
{
    public Fractional Operand => operand;
}
