namespace HM5.App;

internal class Calculator : ICalculator
{
    private delegate double MathAction(double x, double y);
    private double? _operand = null;
    private string? _symbol = null;
    private readonly Stack<double> _previous = [];
    private MathAction? _operation = null;

    public event EventHandler<OperandChangedEventArgs>? OperandChangedEvent;
    public event EventHandler<InvalidOperationArgs>? InvalidOperationEvent;

    public void CancelLast()
    {
        if (_previous.Count > 0)
        {
            _operand = _previous.Pop();
            OperandChanged();
        }
        else
        {
            InvalidOperationEvent?.Invoke(this, new(CalculatorErrors.NotFoundPrevious));
        }
    }

    public void Set(string input)
    {
        if (double.TryParse(input, out double result))
        {
            if (_operand is not null)
                _previous.Push(_operand!.Value);

            if (_operand is null || _operation is null)
            {
                _operand = result;
                OperandChanged();
                return;
            }

            _operand = _operation?.Invoke(_operand.Value, result) ?? 0;
            OperandChanged();
            _operation = null;
            return;
        }

        _operation = GetOperation(input);
        if (_operation is null)
        {
            InvalidOperationEvent?.Invoke(this, new(CalculatorErrors.InvalidInput));
        }
    }

    private MathAction? GetOperation(string input)

        => input switch
        {
            "+" => (x, y) =>
            {
                _symbol = "+";
                return x + y;
            }
            ,
            "-" => (x, y) =>
            {
                _symbol = "-";
                return x - y;
            }
            ,
            "*" => (x, y) =>
            {
                _symbol = "*";
                return x * y;
            }
            ,
            "/" => (x, y) =>
            {
                _symbol = "/";
                if (y == 0)
                {
                    InvalidOperationEvent?.Invoke(this, new(CalculatorErrors.DivideByZero));
                }
                return y != 0 ? x / y : _operand!.Value;
            }
            ,
            _ => null
        };

    private void OperandChanged()
    {
        OperandChangedEvent?.Invoke(this, new(_operand!.Value));
    }
}
