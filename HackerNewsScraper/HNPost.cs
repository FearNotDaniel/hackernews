using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HackerNewsScraper
{
    // Model to represent a post in the form ready for Json output.
    // Naming strategy indicates that C# style properties (Pascal Case) will be
    // serialized with JS-style Camel Case identifiers.
    //
    // Field validation is specified here using standard .NET attributes.
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

        // Collection of errors if any other fields fail validation rules. Property is marked
        // to be excluded from output if no errors are found.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ValidationResult> ValidationErrors { get; set; }
    }
}
