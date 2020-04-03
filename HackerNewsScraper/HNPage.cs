using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace HackerNewsScraper
{
    // Class to represent a page from HN with logic to extract the list of posts and next page link.
    public class HNPage
    {
        public const string BASE_URI = "https://news.ycombinator.com/";
        private HtmlDocument _doc;
        private HNPostValidator _validator = new HNPostValidator(BASE_URI);

        // Raw Html content will be passed by the caller using the HtmlAgilityPack classes.
        public HNPage(HtmlDocument doc)
        {
            _doc = doc;
        }

        // Returns all the posts found on the page, after converting them to the strongly-typed,
        // validated HNPost object.
        public IEnumerable<HNPost> Posts
        {
            get
            {
                // If no itemlist table is found, quietly return an empty list.
                var itemList = _doc.DocumentNode.QuerySelector("table.itemlist");
                if (itemList == null) return new List<HNPost>();

                // Select all the post rows from the table and convert them via the validator class.
                var items = itemList.QuerySelectorAll("tr.athing");
                return items.Select(i => _validator.ConvertAndValidate(new HNPostNode(i)));
            }
        }

        // Returns the absolute Uri string of the following page so more links can be retrieved.
        public string NextPageUri
        {
            get
            {
                var moreLink = _doc.DocumentNode.QuerySelector("a.morelink");
                if (moreLink == null) return "";
                return BASE_URI + moreLink.GetAttributeValue("href", null);
            }
        }
    }
}
