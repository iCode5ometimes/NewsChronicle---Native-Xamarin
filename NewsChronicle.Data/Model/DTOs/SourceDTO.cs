using Newtonsoft.Json;

namespace NewsChronicle.Data.Model.DTOs
{
    public class SourceDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
