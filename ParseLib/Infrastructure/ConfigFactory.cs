using System.Net;
using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Io;
using AngleSharp.Io.Network;

namespace ParseLib.Infrastructure
{
    public static class ConfigFactory
    {
        /* ru-RU uk-UA en-US   USD UAH RUB */
        public static IConfiguration CreateConfigurationIHerb(string lang = "en-US", string ue = "USD")
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader().WithDefaultCookies();
            MemoryCookieProvider? cookieProvider =
                config.Services.First(e => e.GetType() == typeof(MemoryCookieProvider))
                    as MemoryCookieProvider;
            cookieProvider?.Container.Add(new Cookie("ih-preference",
                $"store=0&country=US&currency={ue}&language={lang}",
                "/",
                "iherb.com"));
            cookieProvider?.Container.Add(new Cookie("iher-pref1",
                $"accsave=0&bi=0&ctd=www&ifv=1&ihr-code1=MKT1016&ignoredCookie=uk-UA&storeid=0&sccode=US&lan={lang}&scurcode={ue}&pc=OTI1NzE&whr=2&wp=1",
                "/", "iherb.com"));
            return config;
        }

        public static IConfiguration CreateConfigurationMetanit(string proxy = "", int port = 0)
        {
            HttpClient client = string.IsNullOrWhiteSpace(proxy)
                ? new HttpClient()
                : new HttpClient(new HttpClientHandler()
                {
                    Proxy = new WebProxy(proxy, port),
                    PreAuthenticate = true,
                    UseCookies = true,
                });
            // "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36";
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("Mozilla", "5.0"));
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("Chrome", "98.0.4758.109"));
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("Safari", "537.36"));

            var config = Configuration.Default
                .With(new HttpClientRequester(client))
                .WithTemporaryCookies()
                .WithDefaultLoader();
            return config;
        }
    }
}