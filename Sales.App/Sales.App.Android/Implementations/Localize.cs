[assembly: Xamarin.Forms.Dependency(typeof(Sales.App.Droid.Implementations.Localize))]

namespace Sales.App.Droid.Implementations
{
    using System.Globalization;
    using System.Threading;
    using Helpers;
    using Interfaces;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));
            
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch(CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                }
                catch(CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            switch (androidLanguage)
            {
                case "es-US":
                case "es-MX":
                    netLanguage = "es-MX";
                    break;
                case "it":
                    netLanguage = "it";
                    break;
            }
            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode;
            switch (platCulture.LanguageCode)
            {
                case "es-US":
                case "es-MX":
                    netLanguage = "es-MX";
                    break;
                case "it":
                    netLanguage = "it";
                    break;
            }
            return netLanguage;
        }
    }
}