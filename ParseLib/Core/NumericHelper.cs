using System.Text.RegularExpressions;

namespace ParseLib.Core;

public class NumericHelper
{
    private Dictionary<Type, Func<string, object>> CostumeSwitch { get; }

    public NumericHelper()
    {
        CostumeSwitch = new()
        {
            {typeof(byte), (str) => byte.Parse(str)},
            {typeof(short), (str) => short.Parse(str)},
            {typeof(int), (str) => int.Parse(str)},
            {typeof(long), (str) => long.Parse(str)},
            {typeof(double), (str) => double.Parse(str)},
            {typeof(float), (str) => float.Parse(str)},
            {typeof(decimal), (str) => decimal.Parse(str)}
        };
    }

    public bool IsNumeric(Type type) => CostumeSwitch.Keys.Contains(type);

    public object ParseNumeric(Type type, string value)
    {
        if (!IsNumeric(type))
            throw new ArgumentException($"Type {type.Name} is not numeric");
            
        var cleanString = new Regex(@"\d*\,?\d*").Match(value).Value;
        if (string.IsNullOrEmpty(cleanString))
            throw new Exception($"Failed parse {value} to {type.Name}");

        return CostumeSwitch[type].Invoke(cleanString);
    }

    public bool TryParseNumeric<T>(Type type, string value, out T result)
    {
        var answer = IsNumeric(type);
        if (answer)
            result = (T) ParseNumeric(type, value);
        else
            result = default;
            
        return answer;
    }
}