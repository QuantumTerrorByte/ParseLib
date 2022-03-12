using System.Net;
using AngleSharp;
using AngleSharp.Io;

namespace ParseLib.Core
{
    public static class ConfigFactory
    {
        /* ru-RU uk-UA en-US   USD UAH RUB */
        public static IConfiguration CreateConfigurationIHerb(string lang = "en-US", string ue = "USD")
        {
            IConfiguration result = Configuration.Default.WithDefaultLoader().WithDefaultCookies();
            MemoryCookieProvider? cookieProvider = result.Services.First(e => e.GetType() == typeof(MemoryCookieProvider))
                as MemoryCookieProvider;
            cookieProvider?.Container.Add(new Cookie("ih-preference", $"store=0&country=US&currency={ue}&language={lang}",
                "/",
                "iherb.com"));
            cookieProvider?.Container.Add(new Cookie("iher-pref1",
                $"accsave=0&bi=0&ctd=www&ifv=1&ihr-code1=MKT1016&ignoredCookie=uk-UA&storeid=0&sccode=US&lan={lang}&scurcode={ue}&pc=OTI1NzE&whr=2&wp=1",
                "/", "iherb.com"));
            return result;
        }
    }
}