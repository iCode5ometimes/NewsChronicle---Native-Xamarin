using System.Threading;
using NewsChronicle.Data.Interfaces;
using Xamarin.Essentials;

namespace NewsChronicle.Data.Services
{
    public class AppLanguageSetting : IAppLanguageSetting
    {
        public AppLanguageSetting()
        {
        }

        public string GetAppArticleLanguage()
        {
            //in case there is no language set yet (app was fresly installed) then pick the default
            //device language for the article search
            var defaultLanguage =  Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            return Preferences.Get(Constants.Constants.LanguagePreferenceForArticles,
                       defaultLanguage);
        }

        public void SetAppArticleLanguage(string language)
        {
            Preferences.Set(Constants.Constants.LanguagePreferenceForArticles, language);
        }
    }
}
