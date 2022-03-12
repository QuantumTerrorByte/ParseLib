namespace ParseLib.Models
{
    public class CommandGetBlocks : ICommand
    {
        public ParseOptions Data { get; }

        public CommandGetBlocks(ParseOptions payload)
        {
            Data = payload;
        }
    }

    public class CommandMap : ICommand
    {
        public Dictionary<string, ParseOptions> Data { get; } // MappingData 

        public CommandMap(Dictionary<string, ParseOptions> mappingData)
        {
            Data = mappingData;
        }
    }

    

    public interface ICommand
    {
    }
}