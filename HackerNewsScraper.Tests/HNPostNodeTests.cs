using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Xunit;

namespace HackerNewsScraper.Tests
{
    public class HNPostNodeTests
    {
        public const string NORMAL_POST = @"SampleInput\normal-post.html";
        public const string POST_WITHOUT_AUTHOR_OR_POINTS = @"SampleInput\post-without-author-or-points.html";
        public const string POST_WITHOUT_COMMENTS = @"SampleInput\post-without-comments.html";
        
        [Fact]
        public void PostNodeRetrievesTitle()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve title from the node
            var title = node.Title;

            // ASSERT: should find correct title
            Assert.Equal("‘War Dialing’ tool exposes Zoom’s password problems", title);
        }

        [Fact]
        public void PostNodeRetrievesUri()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve uri from the node
            var uri = node.Uri;

            // ASSERT: should find correct uri
            Assert.Equal("https://krebsonsecurity.com/2020/04/war-dialing-tool-exposes-zooms-password-problems/", uri);
        }

        [Fact]
        public void PostNodeRetrievesAuthor()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve author from the node
            var author = node.Author;

            // ASSERT: should find correct author
            Assert.Equal("feross", author);
        }

        [Fact]
        public void PostNodeRetrievesPoints()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve points from the node
            var points = node.Points;

            // ASSERT: should find correct points
            Assert.Equal(476, points);
        }

        [Fact]
        public void PostNodeRetrievesComments()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve points from the node
            var comments = node.Comments;

            // ASSERT: should find correct points
            Assert.Equal(206, comments);
        }

        [Fact]
        public void PostNodeRetrievesRank()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(NORMAL_POST);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve points from the node
            var rank = node.Rank;

            // ASSERT: should find correct points
            Assert.Equal(4, rank);
        }

        [Fact]
        public void PostNodeSurvivesMissingAuthor()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(POST_WITHOUT_AUTHOR_OR_POINTS);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve author from the node
            var author = node.Author;

            // ASSERT: should run OK with missing author node
            Assert.Equal("", author);
        }

        [Fact]
        public void PostNodeSurvivesMissingPoints()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(POST_WITHOUT_AUTHOR_OR_POINTS);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve points from the node
            var points = node.Points;

            // ASSERT: should run OK with missing points node
            Assert.Equal(0, points);
        }

        [Fact]
        public void PostNodeSurvivesMissingComments()
        {
            // ARRANGE: load sample Html from file into an HNPostNode object
            var doc = new HtmlDocument();
            doc.Load(POST_WITHOUT_COMMENTS);
            var node = new HNPostNode(doc.DocumentNode.QuerySelector("tr.athing"));

            // ACT: retrieve comments from the node
            var comments = node.Comments;

            // ASSERT: should run OK with missing points node
            Assert.Equal(0, comments);
        }
    }
}
