using Newtonsoft.Json;

namespace SpellChecker.Data_Models
{
    public class WordHistory
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Words")]
        public string Words { get; set; }

    }
}
