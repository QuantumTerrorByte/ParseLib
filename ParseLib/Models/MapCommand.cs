namespace ParseLib.Models;

public record MapCommand(
    string FieldName,
    string Selector,
    int SkipStart = 0,
    int SkipEnd = 0,
    bool EnableDefaultValue = false
);