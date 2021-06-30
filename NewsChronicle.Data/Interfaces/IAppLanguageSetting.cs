namespace NewsChronicle.Data.Interfaces
{
    public interface IAppLanguageSetting
    {
        void SetAppArticleLanguage(string language);

        string GetAppArticleLanguage();
    }
}
