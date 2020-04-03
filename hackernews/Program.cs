using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using HackerNewsScraper;

namespace hackernews
{
    [Command(ExtendedHelpText = @"
Remarks:
    Scrapes the top N items from Hacker News and outputs the results as validated Json.")]
    class Program
    {
        // Invoke the OnExecute() method via CommandLineUtils' framework to parse and validate the arguments.
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        
        [Required]
        [PostsValidation]
        [Option(Description = "How many posts to print. A positive integer <= 100.")]
        public int Posts { get; }

        [Option(Description = "Suppress from the output any posts that fail validation, for example when there is no post author.")]
        public bool OmitValidationFailures { get; }

        private void OnExecute()
        {
            var scraper = new HNScraper();
            Console.WriteLine(scraper.GetPosts(this.Posts, this.OmitValidationFailures));
        }
    }

    class PostsValidationAttribute : ValidationAttribute
    {
        public PostsValidationAttribute() : base("The value for {0} must be a positive integer <= 100.") {}

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!int.TryParse((string)value, out int posts) || posts < 1 || posts > 100)
            {
                return new ValidationResult(FormatErrorMessage(context.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
