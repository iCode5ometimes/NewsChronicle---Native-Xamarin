using System;
using Newtonsoft.Json;

namespace NewsChronicle.Data.Model.DTOs
{
    public class ArticleDTO
    {
        [JsonProperty("source")]
        public SourceDTO Source { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("urlToImage")]
        public string UrlToImage { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
