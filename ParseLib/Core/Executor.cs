using AngleSharp.Dom;
using ParseLib.Models;

namespace ParseLib.Core
{
    public class Executor <T> 
    {
        
        public T Execute(IDocument document, List<ICommand> commands)
        {
            return (T) new object();
        }
    }
}