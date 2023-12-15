namespace HM7;


internal class TestClass
{
    [CustomName("integer_property")]
    public int IntegerProperty { get; set; }
    [CustomName("string_property")]
    public string? StringProperty { get; set; }
    [CustomName("decimal_property")]
    public decimal DecimalProperty { get; set; }
    [CustomName("char_array_property")]
    public char[]? CharArrayProperty { get; set; }

    public TestClass()
    { }
    private TestClass(int integerValue)
    {
        this.IntegerProperty = integerValue;
    }
    public TestClass(int integerValue, string stringValue, decimal decimalValue, char[] charArrayValue) : this(integerValue)
    {
        this.StringProperty = stringValue;
        this.DecimalProperty = decimalValue;
        this.CharArrayProperty = charArrayValue;
    }
}
