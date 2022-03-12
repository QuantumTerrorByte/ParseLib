// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io.Network;
using ParseLib.Core;
using ParseLib.Infrastructure;

namespace ParseLib
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // await Test.TestSelectors();
            // var parser = new ParserIHerb(BrowsingContext.New(ConfigFactory.CreateConfigurationIHerb()));
            // await parser.Parse("https://www.iherb.com/c/supplements");
            // Console.WriteLine(typeof(Dictionary<string, int>).IsAssignableTo(typeof(IEnumerable)));
            // Console.WriteLine(typeof(ParserIHerb).IsAssignableTo(typeof(IEnumerable)));
            /*Console.WriteLine(typeof(byte));
            Console.WriteLine(typeof(short));
            Console.WriteLine(typeof(int));
            Console.WriteLine(typeof(long));
            Console.WriteLine(typeof(double));
            Console.WriteLine(typeof(float));
            Console.WriteLine(typeof(decimal));*/
            var helper = new NumericHelper();
            decimal dec = 1.22m;
            double d = 1.22;
            long l = 23816293;
            Console.WriteLine(helper.IsNumeric(typeof(Int32)));
            Console.WriteLine(helper.IsNumeric(dec.GetType()));
            Console.WriteLine(helper.IsNumeric(d.GetType()));
            Console.WriteLine(helper.IsNumeric(l.GetType()));
            var temp1 = helper.ParseNumeric(dec.GetType(), "123,11");
            var temp2 = helper.ParseNumeric(d.GetType(), "123,1");
            var temp3 = helper.ParseNumeric(l.GetType(), "123");
            Console.WriteLine(temp1.GetType().FullName);
            Console.WriteLine(temp2.GetType().FullName);
            Console.WriteLine(temp3.GetType().FullName);
            Console.WriteLine(helper.IsNumeric("asd".GetType()));
            Console.WriteLine(helper.IsNumeric(new MapCommand("", "").GetType()));
        }

        //todo performance get all props vs single?
        public static void Map(IDocument document, object target, List<MapCommand> commands)
        {
            try
            {
                var targetType = target.GetType();
                foreach (var (fieldName, selector, skipStart, skipEnd) in commands)
                {
                    var targetProp = targetType.GetProperty(fieldName) ??
                                     throw new($"Property with name {fieldName} not found");
                    var targetPropType = targetProp.PropertyType;

                    if (targetPropType == typeof(string))
                    {
                        var selectorResult = document.QuerySelector(selector) ??
                                             throw new($"QuerySelector {fieldName} not found");
                        targetProp.SetValue(target, selectorResult.InnerHtml);
                    }
                    else if (targetPropType.IsValueType)
                    {
                    }
                    else if (targetPropType.IsAssignableTo(typeof(IEnumerable)))
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static T ParseNumb<T>(Type type, string arg) where T : struct
        {
            T result = default;
            var list = new List<string>()
            {
            };
            return result;
        }
    }

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

        public bool TryParseNumeric<T>(Type type, string value, out T result) where T : struct
        {
            var answer = IsNumeric(type);
            if (answer)
                result = (T) ParseNumeric(type, value);
            else
                result = default;
            
            return answer;
        }
    }

    public record MapCommand(
        string FieldName,
        string Selector,
        int SkipStart = 0,
        int SkipEnd = 0
    );

    public class Test
    {
        public static async Task TestSelectors()
        {
            using StreamReader reader = new(@"\Projects\ParseLib\ParseLib\src.html");
            string html = await reader.ReadToEndAsync();

            IBrowsingContext context = BrowsingContext.New(ConfigFactory.CreateConfigurationIHerb());
            // var document = await context.OpenAsync("https://www.iherb.com/c/supplements");
            IDocument document = await context.OpenAsync(req => req.Content(html));
            // Console.WriteLine(document.GetElementsByClassName("fourth third").Length);
            document.QuerySelectorAll("div.second div").For(e => Console.WriteLine(e.InnerHtml));
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine(document.QuerySelector("div.fourth.third").InnerHtml);
            Console.WriteLine(document.QuerySelector("div.fifth .fourth").InnerHtml);
            Console.WriteLine(document.QuerySelector("div.fifth div.fourth").InnerHtml);
            Console.WriteLine(document.QuerySelector("div.third").InnerHtml);
            Console.WriteLine(document.QuerySelector("span.third").InnerHtml);
            Console.WriteLine(document.QuerySelector("#target").InnerHtml);
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (IElement element in document.QuerySelectorAll("div.third"))
            {
                Console.WriteLine(element.InnerHtml);
            }
        }

        public static async Task Test1()
        {
            try
            {
                HttpClientHandler handler = new()
                {
                    Proxy = new WebProxy("18.216.136.190", 9090), //"85.25.235.229", 5566
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                    UseProxy = false,
                };

                IConfiguration config = Configuration.Default
                    .WithRequesters(handler)
                    .WithDefaultLoader()
                    .WithTemporaryCookies()
                    .WithDefaultLoader();
                IBrowsingContext context = BrowsingContext.New(config);
                IDocument document = await context.OpenAsync("https://professorweb.ru/my/csharp/web/level2/2_7.php");
                // "https://metanit.com/sharp/tutorial/1.5.php"

                await using (StreamWriter writer = new(@"D:\test\Second.html", false))
                {
                    await writer.WriteAsync(document.DocumentElement.OuterHtml);
                }

                Console.WriteLine(document.DocumentElement.OuterHtml.Length);
                Console.WriteLine(
                    "=====================================================================================");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task Test2()
        {
            try
            {
                HttpClientHandler handler = new()
                {
                    Proxy = new WebProxy("190.2.131.209"),
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                    UseCookies = true,
                    UseProxy = false,
                };
                HttpClient client = new(handler);
                client.DefaultRequestHeaders.UserAgent.Add(
                    new("Mozilla", "5.0"));
                client.DefaultRequestHeaders.UserAgent.Add(
                    new("Chrome", "98.0.4758.109"));
                client.DefaultRequestHeaders.UserAgent.Add(
                    new("Safari", "537.36"));
                // "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36";
                // ... and the others if you want, even though `content-type` etc. should / will be determined by AngleSharp
                IConfiguration config = Configuration.Default
                    .With(new HttpClientRequester(client))
                    .WithTemporaryCookies()
                    .WithDefaultLoader();
                IBrowsingContext context = BrowsingContext.New(config);
                IDocument doc = await context.OpenAsync("https://metanit.com/sharp/tutorial/1.5.php");

                await using (StreamWriter writer = new(@"D:\test\First.html", false))
                {
                    await writer.WriteAsync(doc.DocumentElement.OuterHtml);
                }

                Console.WriteLine(doc.DocumentElement.OuterHtml);
                Console.WriteLine(
                    "=====================================================================================");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}