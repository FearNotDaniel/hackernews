using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HackerNewsScraper
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class HNPost
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Url]
        public string Uri { get; set; }

        [Required]
        [StringLength(256)]
        public string Author { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} must be an integer >= 0.")]
        public int Points { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} must be an integer >= 0.")]
        public int Comments { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} must be an integer >= 0.")]
        public int Rank { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ValidationResult> ValidationErrors { get; set; }
    }
}
