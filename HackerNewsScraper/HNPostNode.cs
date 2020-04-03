using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace HackerNewsScraper
{
    public class HNPostNode
    {
        private HtmlNode _itemNode;
        private HtmlNode _subtextNode;
        
        public HNPostNode(HtmlNode itemNode)
        {
            _itemNode = itemNode;
            _subtextNode = _itemNode.NextSibling.QuerySelector("td.subtext");
        }

        public string Id { get { return _itemNode.Id; } }

        public string Title
        {
            get
            {
                var titleLink = _itemNode.QuerySelector("td.title>a");
                if (titleLink == null) return "";
                return titleLink.InnerText;
            }
        }

        public string Uri
        {
            get
            {
                var titleLink = _itemNode.QuerySelector("td.title>a");
                if (titleLink == null) return "";
                return titleLink.GetAttributeValue("href", null);
            }
        }

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
