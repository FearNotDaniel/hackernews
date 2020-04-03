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

            if (omitValidationFailures) posts = posts.Where(p => p.ValidationErrors == null).ToList();
            return JsonConvert.SerializeObject(posts.Take(requiredPosts), Formatting.Indented);
        }
    }
}
