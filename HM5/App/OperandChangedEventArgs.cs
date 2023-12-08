namespace HM5.App;

public class OperandChangedEventArgs(double operand) : EventArgs
{
    public double Operand => operand;
}
