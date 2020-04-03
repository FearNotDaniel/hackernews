using System.IO;
using System.Linq;
using System.Reflection;
using HtmlAgilityPack;
using Xunit;

namespace HackerNewsScraper.Tests
{
    public class HNPageTests
    {
        public const string NEWS_PAGE = "HackerNewsScraper.Tests.SampleInput.news-page.html";
        public const string NEWS_PAGE_WITHOUT_POSTS_OR_MORE = "HackerNewsScraper.Tests.SampleInput.news-page-without-posts-or-more.html";

        [Fact]
        public void PageSelectsAllPosts()
        {
            // ARRANGE: load sample Html from file into an HNPage object
            var doc = ResourceLoader.LoadHtmlDocument(NEWS_PAGE);
            var page = new HNPage(doc);

            // ACT: retrieve posts from the page
            var posts = page.Posts;

            // ASSERT: should find 30 posts on page
            Assert.Equal(30, posts.Count());
        }

        [Fact]
        public void PageSelectsMoreLink()
        {
            // ARRANGE: load sample Html from file into an HNPage object
            var doc = ResourceLoader.LoadHtmlDocument(NEWS_PAGE);
            var page = new HNPage(doc);

            // ACT: retrieve next page uri
            var uri = page.NextPageUri;

            // ASSERT: should find next page link
            Assert.Equal(HNPage.BASE_URI + "news?p=2", uri);
        }

        [Fact]
        public void PageSurvivesMissingMoreLink()
        {
            // ARRANGE: load sample Html from file into an HNPage object
            var doc = ResourceLoader.LoadHtmlDocument(NEWS_PAGE_WITHOUT_POSTS_OR_MORE);
            var page = new HNPage(doc);

            // ACT: retrieve next page uri
            var uri = page.NextPageUri;

            // ASSERT: next page link should be empty but no exception thrown
            Assert.Equal("", uri);
        }

        [Fact]
        public void PageSurvivesMissingPostsTable()
        {
            // ARRANGE: load sample Html from file into an HNPage object
            var doc = ResourceLoader.LoadHtmlDocument(NEWS_PAGE_WITHOUT_POSTS_OR_MORE);
            var page = new HNPage(doc);

            // ACT: retrieve posts from a page that has no posts
            var posts = page.Posts;

            // ASSERT: posts collection is empty, but no exception has been thrown
            Assert.Empty(posts);
        }
    }
}
