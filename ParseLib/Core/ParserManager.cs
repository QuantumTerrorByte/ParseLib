using AngleSharp;
using ParseLib.Core.Interfaces;
using ParseLib.Models;

namespace ParseLib.Core
{
    public class ParserManager<T> where T : class, IParser
    {
        public IParser Parser { get; set; }
        public IBrowsingContext Context { get; set; }

        public ParserManager(IBrowsingContext context)
        {
            var ctor = typeof(T).GetConstructor(new[] {context.GetType()});
                // (T) Activator.CreateInstance(typeof(T), context);
            Context = context;
        }

        public string GetPage()
        {
            return "";
        }

        // public List<T> Parse(string url, string selector, bool isUrl = false);
        // {
        //     for (int i = Options.Start; i < Options.End; i++)
        //     {
        //         string dom = GetPage();
        //         foreach (IParser<T> parser in ChainOfParsers)
        //         {
        //             parser.Parse();
        //         }
        //     }
        //
        //     return Result;
        // }
    }
}