using System.Collections;
using System.Net;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io.Network;

namespace ParseLib.Trash;

static class TestVPN
{
    public static async Task RefUrl()
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
            IDocument document =
                await context.OpenAsync("https://professorweb.ru/my/csharp/web/level2/2_7.php");
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

    public static async Task Metanit()
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

    public static void Temp()
    {
        Console.WriteLine("=============================================================");
            Console.WriteLine(typeof(string[]).IsAssignableTo(typeof(IEnumerable)));
            Console.WriteLine(typeof(List<int>).IsAssignableTo(typeof(IEnumerable)));
            Console.WriteLine(typeof(IEnumerable<string>).IsAssignableTo(typeof(IEnumerable)));
            Console.WriteLine(typeof(ICollection<int>).IsAssignableTo(typeof(IEnumerable)));
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(typeof(IEnumerable).IsAssignableTo(typeof(string[])));
            Console.WriteLine(typeof(IEnumerable).IsAssignableTo(typeof(List<string>)));
            Console.WriteLine(typeof(IEnumerable).IsAssignableTo(typeof(IEnumerable<string>)));
            Console.WriteLine(typeof(IEnumerable).IsAssignableTo(typeof(ICollection<int>)));
            Console.WriteLine("=============================================================");
            Console.WriteLine(typeof(string[]).IsAssignableFrom(typeof(IEnumerable)));
            Console.WriteLine(typeof(List<int>).IsAssignableFrom(typeof(IEnumerable)));
            Console.WriteLine(typeof(IEnumerable<string>).IsAssignableFrom(typeof(IEnumerable)));
            Console.WriteLine(typeof(ICollection<int>).IsAssignableFrom(typeof(IEnumerable)));
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(typeof(IEnumerable).IsAssignableFrom(typeof(string[])));
            Console.WriteLine(typeof(IEnumerable).IsAssignableFrom(typeof(List<string>)));
            Console.WriteLine(typeof(IEnumerable).IsAssignableFrom(typeof(IEnumerable<string>)));
            Console.WriteLine(typeof(IEnumerable).IsAssignableFrom(typeof(ICollection<int>)));
            Console.WriteLine("=============================================================");
            Console.WriteLine(typeof(string[]).IsInstanceOfType(typeof(IEnumerable)));
            Console.WriteLine(typeof(List<int>).IsInstanceOfType(typeof(IEnumerable)));
            Console.WriteLine(typeof(IEnumerable<string>).IsInstanceOfType(typeof(IEnumerable)));
            Console.WriteLine(typeof(ICollection<int>).IsInstanceOfType(typeof(IEnumerable)));
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(typeof(IEnumerable).IsInstanceOfType(typeof(string[])));
            Console.WriteLine(typeof(IEnumerable).IsInstanceOfType(typeof(List<string>)));
            Console.WriteLine(typeof(IEnumerable).IsInstanceOfType(typeof(IEnumerable<string>)));
            Console.WriteLine(typeof(IEnumerable).IsInstanceOfType(typeof(ICollection<int>)));
            Console.WriteLine("=============================================================");
    }
}