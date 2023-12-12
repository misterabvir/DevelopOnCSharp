namespace HM6.App;

internal class Calculator : ICalculator
{
    private delegate Fractional MathAction(Fractional x, Fractional y);
    private Fractional? _operand = null;
    private string? _symbol = null;
    private readonly Stack<Fractional> _previous = [];
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
        if (Fractional.TryParse(input, out Fractional? result))
        {
            if (_operand is not null)
                _previous.Push(_operand);

            if (_operand is null || _operation is null)
            {
                _operand = result;
                OperandChanged();
                return;
            }

            _operand = _operation?.Invoke(_operand, result!) ?? 0;
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
                return  x / y;
            }
            ,
            _ => null
        };

    private void OperandChanged()
    {
        OperandChangedEvent?.Invoke(this, new(_operand!));
    }
}
