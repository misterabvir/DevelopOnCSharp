namespace BitLogic;

public class Bits
{
    private const string ONE = "1";
    private const string ZERO = "0";
    private bool[] bits = [];
    private Bits() { }

    public override string ToString() =>
        string.Join("", bits.SkipWhile(b => !b).Select(b => b ? ONE : ZERO));

    public static implicit operator Bits(long value) =>
        new()
        {
            bits = Enumerable
                .Range(0, 32)
                .Select(index => (value & (1L << index)) != 0)
                .Reverse()
                .ToArray()
        };

    public static explicit operator long(Bits value) =>
        value.bits
            .Reverse()
            .Select((bit, index) => bit ? (1L << index) : 0)
            .Aggregate((current, next) => current | next);

    public static implicit operator Bits(int value) => (long)value;
    public static explicit operator int(Bits value) => (int)(long)value;

    public static implicit operator Bits(byte value) => (long)value;
    public static explicit operator byte(Bits value) => (byte)(long)value;

}
