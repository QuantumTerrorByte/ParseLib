using ParseLib.Infrastructure;

namespace ParseLib.Models;

/// <summary>
/// parse route
/// </summary>
public class ParseOptions
{
    public Type DataType { get; set; }
    public Dictionary<string, SearchType> Type { get; set; }
}