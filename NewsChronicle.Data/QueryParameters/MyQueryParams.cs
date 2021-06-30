using System;
using NewsChronicle.Data.Enum;
using Refit;

namespace NewsChronicle.Data.QueryParameters
{
    public class MyQueryParams
    {
        #region Properties

        [AliasAs("apiKey")]
        public string ApiKey
        {
            get => "89f11336edb24526908eef8af39b279f";
        }

        [AliasAs("pageSize")]
        public string PageSize
        {
            get => "100";
        }

        [AliasAs("language")]
        public string Language { get; set; }

        [AliasAs("sortBy")]
        public string SortBy { get; set; }

        [AliasAs("category")]
        public string Category { get; set; }

        // this api looks for keywords or phrases in the article title and its body
        [AliasAs("q")]
        public string KeywordsOrPhrases { get; set; }

        public ArticleApiCall ApiCallType { get; set; } = ArticleApiCall.TopHeadlines;

        #endregion

        #region Methods

        public static MyQueryParams GetCopy(MyQueryParams queryParams)
        {
            if (queryParams != null)
            {
                return new MyQueryParams
                {
                    Language = queryParams.Language,
                    SortBy = queryParams.SortBy,
                    Category = queryParams.Category,
                    KeywordsOrPhrases = queryParams.KeywordsOrPhrases,
                    ApiCallType = queryParams.ApiCallType
                };
            }
            return new MyQueryParams();
        }

        public override bool Equals(object obj)
        {
            return obj is MyQueryParams @params &&
                   ApiKey == @params.ApiKey &&
                   PageSize == @params.PageSize &&
                   Language == @params.Language &&
                   SortBy == @params.SortBy &&
                   Category == @params.Category &&
                   ApiCallType == @params.ApiCallType &&
                   KeywordsOrPhrases == @params.KeywordsOrPhrases;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ApiKey,
                                    PageSize,
                                    Language,
                                    SortBy,
                                    Category,
                                    KeywordsOrPhrases,
                                    ApiCallType);
        }

        #endregion
    }
}
