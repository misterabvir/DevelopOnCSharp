namespace HM6.App;

public class InvalidOperationArgs(string message) : EventArgs
{
    public string Message => message;
}
