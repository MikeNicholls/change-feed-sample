using Newtonsoft.Json;

namespace Company.Function
{
    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
