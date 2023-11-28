using BitLogic;

long longValue = 1234567890L;
Bits bitLongValue = longValue;
Console.WriteLine($"Long = {longValue} => Bits = {bitLongValue}");
long longValueFromBits = (long)bitLongValue;
Console.WriteLine($"Bits = {bitLongValue} => Long = {longValueFromBits}");
/*
Long = 1234567890 => Bits = 1001001100101100000001011010010
Bits = 1001001100101100000001011010010 => Long = 1234567890
*/

int intValue = 123456789;
Bits bitIntValue = intValue;
Console.WriteLine($"Integer = {intValue} => Bits = {bitIntValue}");
int intValueFromBits = (int)bitIntValue;
Console.WriteLine($"Bits = {bitIntValue} => Integer = {intValueFromBits}");
/*
Integer = 123456789 => Bits = 111010110111100110100010101
Bits = 111010110111100110100010101 => Integer = 123456789
*/

byte byteValue = 123;
Bits bitByteValue = byteValue;
Console.WriteLine($"Byte = {byteValue} => Bits = {bitByteValue}");
byte byteValueFromBits = (byte)bitByteValue;
Console.WriteLine($"Bits = {bitByteValue} => Byte = {byteValueFromBits}");
/*
Byte = 123 => Bits = 1111011
Bits = 1111011 => Byte = 123
*/