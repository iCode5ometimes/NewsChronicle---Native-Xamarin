namespace NewsChronicle.Data.Constants
{
    public static class Constants
    {
        public static readonly string KeywordWildCardForAllArticlesSearch = "*";  // used when there is no word to search for when the app looks
                                                                                  // for all articles, it is mandatory to use this otherwise
                                                                                  // the endpoint wont work

        public static readonly string LanguagePreferenceForArticles = "langPref";

        public static readonly string ArticleDetailPageSegueIdentifier = "articleDetail";

        public static readonly string LocalDatabaseFileName = "articles.db3";

        public static readonly string ArticleSelectedIntentName = "ClickedArticle";

        public static readonly string LanguagePreferenceIntentName = "newLang";
    }
}
