using System.Collections.Generic;
using Newtonsoft.Json;

namespace NewsChronicle.Data.Model.DTOs
{
    public class ArticleListDTO
    {
        [JsonProperty("articles")]
        public List<ArticleDTO> Articles { get; set; }
    }
}
