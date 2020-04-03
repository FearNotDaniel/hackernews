using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace HackerNewsScraper
{
    public class HNPage
    {
        public const string BASE_URI = "https://news.ycombinator.com/";
        private HtmlDocument _doc;
        private HNPostValidator _validator = new HNPostValidator(BASE_URI);

        public HNPage(HtmlDocument doc)
        {
            _doc = doc;
        }

        public IEnumerable<HNPost> Posts
        {
            get
            {
                var itemList = _doc.DocumentNode.QuerySelector("table.itemlist");
                if (itemList == null) return new List<HNPost>();

                var items = itemList.QuerySelectorAll("tr.athing");
                return items.Select(i => _validator.ConvertAndValidate(new HNPostNode(i)));
            }
        }

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
