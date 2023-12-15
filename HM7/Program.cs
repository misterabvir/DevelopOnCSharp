using System.Reflection;
using System.Text;
using HM7;

const string SEMICOLUMN = ": ";
const string COMA = ", ";
const string EQUAL = " = ";
const string EMPTY = "";
const char STRAIGHT_LEFT_BRACKET = '[';
const char STRAIGHT_RIGHT_BRACKET = ']';

int integerInput = 5;
string StringInput = "string";
decimal decimalInput = 3m;
char[] charArrayInput = ['c', 'h', 'a', 'r', 's'];

var v1 = DefaultPublicConstructor();
var v2 = PrivateConstructorParameterLess(integerInput);
var v3 = PublicConstructorParameterFull(integerInput, StringInput, decimalInput, charArrayInput);

string fromObject = ObjectToString(v3);
Console.WriteLine(fromObject);
//HM7.TestClass, HM7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null: IntegerProperty[integer_property] = 5, StringProperty[string_property] = string, DecimalProperty[decimal_property] = 3, CharArrayProperty[char_array_property] = chars

TestClass fromString = (TestClass)StringToObject(fromObject)!;
Console.WriteLine($"attributeNameValue: {nameof(fromString)}");
Console.WriteLine($"type: {fromString!.GetType().FullName}");
Console.WriteLine($"{nameof(fromString.StringProperty)} = {fromString.StringProperty}");
Console.WriteLine($"{nameof(fromString.IntegerProperty)} = {fromString.IntegerProperty}");
Console.WriteLine($"{nameof(fromString.DecimalProperty)} = {fromString.DecimalProperty}");
Console.WriteLine($"{nameof(fromString.CharArrayProperty)} = [{string.Join(", ", fromString.CharArrayProperty!)}]");
/*
attributeNameValue: fromString
type: HM7.TestClass
StringProperty = string
IntegerProperty = 5
DecimalProperty = 3
CharArrayProperty = [c, h, a, r, s]
*/


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
TestClass PublicConstructorParameterFull(
    int integerValue, 
    string stringValue, 
    decimal decimalValue,
    char[] charArrayValue)
        => (TestClass)Activator.CreateInstance(
                typeof(TestClass), 
                integerValue, 
                stringValue, 
                decimalValue, 
                charArrayValue)!;


TestClass PrivateConstructorParameterLess(int i)
{
    ConstructorInfo ctor = typeof(TestClass).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();
    return (TestClass)ctor.Invoke([i]);
}

TestClass DefaultPublicConstructor()
    => (TestClass)Activator.CreateInstance(typeof(TestClass))!;


object? StringToObject(string s)
{
    var lines = s
        .Split(SEMICOLUMN)
        .Select(line => line.Trim())
        .ToArray();
    var names = lines[0]
        .Split(COMA)
        .Select(line => line.Trim())
        .ToArray();
    string className = names[0];
    string assembly = names[1];

    var instance = Activator
        .CreateInstance(assembly, className)!
        .Unwrap()!;
    var type = instance!.GetType();
    var props = type.GetProperties();

    string[] properties = lines[1]
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();

    foreach (var property in properties)
    {
        string[] keyValue = property.Split(EQUAL, StringSplitOptions.RemoveEmptyEntries);

        if (keyValue.Length != 2)
        {
            return null;
        }

        //set value in property use propertyName
        //string attributeNameValue = keyValue[0].Trim().Split(STRAIGHT_LEFT_BRACKET)[0];
        //string value = keyValue[1].Trim();
        //var prop = props.FirstOrDefault(p => p.Name.Equals(attributeNameValue, StringComparison.OrdinalIgnoreCase));
        //if (prop != null)
        //{
        //    var parsedValue = ParseValue(value, prop.PropertyType);
        //    prop.SetValue(instance, parsedValue);
        //}

        //set value in property use CustomNameAttribute
        string attributeNameValue = keyValue[0].Trim().Split(STRAIGHT_LEFT_BRACKET)[1].TrimEnd(STRAIGHT_RIGHT_BRACKET);
        string value = keyValue[1].Trim();
        var prop = props.First(p => p.GetCustomAttribute<CustomNameAttribute>()!.CustomPropertyName.Equals(attributeNameValue, StringComparison.OrdinalIgnoreCase));
        if (prop != null)
        {
            var parsedValue = ParseValue(value, prop.PropertyType);
            prop.SetValue(instance, parsedValue);
        }

    }

    return instance;
}

object ParseValue(string value, Type targetType)
{
    return Type.GetTypeCode(targetType) switch
    {
        TypeCode.Int32 => int.Parse(value),
        TypeCode.Decimal => decimal.Parse(value),
        TypeCode.String => value,
        TypeCode.Object when targetType == typeof(char[]) => value.ToCharArray(),
        _ => throw new ArgumentException($"Failed to convert value '{value}' to type '{targetType!.Name}'"),
    };
}

string ObjectToString(object instance)
{
    Type type = instance.GetType();
    StringBuilder builder = new();
    builder.Append(type.AssemblyQualifiedName).Append(SEMICOLUMN);
    var properties = type.GetProperties();
    foreach (var property in type.GetProperties())
    {
        //get value of attribute
        var attr = property.GetCustomAttribute<CustomNameAttribute>();        
        builder.Append(property.Name).Append($"{STRAIGHT_LEFT_BRACKET}{attr?.CustomPropertyName}{STRAIGHT_RIGHT_BRACKET}").Append(EQUAL);
        if (property.GetValue(instance) is char[] array)
            builder.Append(string.Join(EMPTY, array));
        else
            builder.Append(property.GetValue(instance));
        if(property != properties.Last())       
            builder.Append(COMA);
    }
    return builder.ToString();
}