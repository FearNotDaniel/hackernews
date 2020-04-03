using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackerNewsScraper
{
    // Utility class to leverage automatic field validation via DataAnnotations on the HNPost model
    public class HNPostValidator
    {
        // The base Uri is required when converting relative links (self posts) back to absolute links.
        private Uri _baseUri;

        public HNPostValidator(string baseUri)
        {
            // Ensure the baseUri supplied is actually a valid, absolute Uri
            if (!Uri.TryCreate(baseUri, UriKind.Absolute, out _baseUri)) throw new ArgumentException("baseUri must be a valid absolute Uri");
        }

        public HNPost ConvertAndValidate(HNPostNode node)
        {
            // Pass the raw values from Html into a validated HNPost object
            var post = new HNPost
            {
                Title = node.Title,
                // Convert relative Uri to absolute; if still not valid, pass through the original value and allow the Validator to pick it up
                Uri = Uri.TryCreate(node.Uri, UriKind.Absolute, out Uri result) || Uri.TryCreate(_baseUri, node.Uri, out result) ? result.AbsoluteUri : node.Uri,
                Author = node.Author,
                Points = node.Points,
                Comments = node.Comments,
                Rank = node.Rank
            };

            // Execute the DataAnnotations validation against the HNPost object
            ValidatePost(post);
            return post;
        }

        public void ValidatePost(HNPost post)
        {
            // Field validation is specified by the DataAnnotations on the HNPost model; any failures will be included in the
            // post's ValidationErrors property, allowing the invalid post to be either inspected or omitted according to user preference.
            var results = new List<ValidationResult>();
            var context = new ValidationContext(post, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(post, context, results, validateAllProperties: true)) post.ValidationErrors = results;
        }
    }
}
