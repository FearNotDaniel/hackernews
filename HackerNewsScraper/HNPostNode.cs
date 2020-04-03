using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace HackerNewsScraper
{
    // Class to represent a post in its raw Html form, with each property parsed from the DOM on demand.
    public class HNPostNode
    {
        private HtmlNode _itemNode;
        private HtmlNode _subtextNode;
        
        public HNPostNode(HtmlNode itemNode)
        {
            // Caller will pass in the HtmlNode that represents the main <tr> element for the post.
            _itemNode = itemNode;

            // Additional fields will be found within the next Html table row, not contained by the
            // main element. Fortunately the HtmlNode retains its document context so we can select
            // it easily with help from Fizzler's CSS selector extensions. 
            _subtextNode = _itemNode.NextSibling.QuerySelector("td.subtext");
        }

        // The post ID is not required for output, but is necessary when identifying the comments counter.
        public string Id { get { return _itemNode.Id; } }

        // As with all properties, Title will fail gracefully if the parent node is not found, returning
        // empty string. This will, however, prompt a validation error on conversion to HNPost object.
        public string Title
        {
            get
            {
                var titleLink = _itemNode.QuerySelector("td.title>a");
                if (titleLink == null) return "";
                return titleLink.InnerText;
            }
        }

        // Uri string is output here in its raw form, which may be absolute or relative depending on the 
        // post type; this will be resolved to an absolute Uri when converting to the output HNPost object.
        public string Uri
        {
            get
            {
                var titleLink = _itemNode.QuerySelector("td.title>a");
                if (titleLink == null) return "";
                return titleLink.GetAttributeValue("href", null);
            }
        }

        // As with all the properties this is protected against "bad" input, i.e. when an author is not
        // indicated; note that occasional valid HN posts do show up on the site without an author.
        // Example: https://news.ycombinator.com/item?id=22756364
        public string Author
        {
            get
            {
                if (_subtextNode == null) return "";
                var userNode = _subtextNode.QuerySelector("a.hnuser");
                if (userNode == null) return "";
                return userNode.InnerText;
            }
        }

        // Occasional posts do not have a points value, as with the authorless example referenced above;
        // property will quietly return 0 when the relevant Html element is missing.
        public int Points
        {
            get
            {
                if (_subtextNode == null) return 0;
                var scoreNode = _subtextNode.QuerySelector("span.score");
                if (scoreNode == null) return 0;
                if (int.TryParse(scoreNode.InnerText.Split(' ')[0], out int result)) return result; else return 0;
            }
        }

        // Occasional posts do not have comments, as with the authorless example referenced above;
        // property will quietly return 0 when the relevant Html element is missing.
        public int Comments
        {
            get
            {
                var commentsNode = _itemNode.NextSibling.QuerySelector($"td.subtext>a[href='item?id={this.Id}']");
                if (commentsNode == null) return 0;
                if (int.TryParse(commentsNode.InnerText.Split('&')[0], out int result)) return result; else return 0;
            }
        }

        public int Rank
        {
            get
            {
                var rankNode = _itemNode.QuerySelector($"td.title>span.rank");
                if (rankNode == null) return 0;
                if (int.TryParse(rankNode.InnerText.Split('.')[0], out int result)) return result; else return 0;
            }
        }
    }
}
