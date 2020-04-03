using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackerNewsScraper
{
    public class HNPostValidator
    {
        private Uri _baseUri;

        public HNPostValidator(string baseUri)
        {
            if (!Uri.TryCreate(baseUri, UriKind.Absolute, out _baseUri)) throw new ArgumentException("baseUri must be a valid absolute Uri");
        }

        public HNPost ConvertAndValidate(HNPostNode node)
        {
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

            ValidatePost(post);
            return post;
        }

        public void ValidatePost(HNPost post)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(post, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(post, context, results, validateAllProperties: true)) post.ValidationErrors = results;
        }
    }
}
