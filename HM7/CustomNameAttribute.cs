namespace HM7;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
internal class CustomNameAttribute(string customPropertyName) : Attribute
{
    public string CustomPropertyName => customPropertyName;
}
