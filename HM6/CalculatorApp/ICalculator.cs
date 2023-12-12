namespace HM6.App;

internal interface ICalculator
{
    void Set(string input);
    event EventHandler<OperandChangedEventArgs>? OperandChangedEvent;
    event EventHandler<InvalidOperationArgs>? InvalidOperationEvent;
    void CancelLast();
}
