using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace HackerNewsScraper
{
    public class HNScraper
    {
        public string GetPosts(int requiredPosts, bool omitValidationFailures)
        {
            var uri = HNPage.BASE_URI;
            var web = new HtmlWeb();
            var posts = new List<HNPost>();

            // Loop through pages until the required number of posts is extracted
            while (posts.Count() < requiredPosts)
            {
                // Load the page and extract the posts
                var hnPage = new HNPage(web.Load(uri));
                posts.AddRange(hnPage.Posts);

                // Go to the next page, as long as a "More" link is found
                if (hnPage.NextPageUri == null) break;
                uri = hnPage.NextPageUri;
            }

            // Posts that fail validation rules are included by default. This is useful because occasional
            // items, presumably added by Y Combinator admins, do not include an author.
            // The command-line switch --omit-validation-failures invokes this line to filter out those posts.
            if (omitValidationFailures) posts = posts.Where(p => p.ValidationErrors == null).ToList();

            // Since posts are harvested in blocks according to page size (30 posts at time of writing,
            // but the app does not assume this will always be true), .Take() clause is used to cut down
            // the output list to the specified number.
            return JsonConvert.SerializeObject(posts.Take(requiredPosts), Formatting.Indented);
        }
    }
}
