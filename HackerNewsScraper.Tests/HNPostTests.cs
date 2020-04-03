using System.Linq;
using HackerNewsScraper;
using Xunit;

namespace HackerNewsScraper.Tests
{
    public class HNPostTests
    {
        [Fact]
        public void AuthorCannotBeEmptyString()
        {
            // ARRANGE: create a post with empty string author
            var post = new HNPost { Author = "" };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Author")).Any());
        }

        [Fact]
        public void AuthorCannotBeNull()
        {
            // ARRANGE: create a post with null author
            var post = new HNPost { Author = null };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Author")).Any());
        }

        [Fact]
        public void AuthorCannotBeOver256Chars()
        {
            // ARRANGE: create a post with author 257 chars long
            var post = new HNPost { Author = new string('a', 257) };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Author")).Any());
        }

        [Fact]
        public void CommentsCannotBeNegative()
        {
            // ARRANGE: create a post with minus Comments
            var post = new HNPost { Comments = -1 };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Comments")).Any());
        }

        [Fact]
        public void PointsCannotBeNegative()
        {
            // ARRANGE: create a post with minus points
            var post = new HNPost { Points = -1 };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Points")).Any());
        }

        [Fact]
        public void RankCannotBeNegative()
        {
            // ARRANGE: create a post with minus Rank
            var post = new HNPost { Rank = -1 };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Rank")).Any());
        }

        [Fact]
        public void TitleCannotBeEmptyString()
        {
            // ARRANGE: create a post with empty string Title
            var post = new HNPost { Title = "" };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Title")).Any());
        }

        [Fact]
        public void TitleCannotBeNull()
        {
            // ARRANGE: create a post with null Title
            var post = new HNPost { Title = null };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Title")).Any());
        }

        [Fact]
        public void TitleCannotBeOver256Chars()
        {
            // ARRANGE: create a post with Title 257 chars long
            var post = new HNPost { Title = new string('a', 257) };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Title")).Any());
        }

        [Fact]
        public void UriIsAValidUri()
        {
            // ARRANGE: create a post with invalid Uri
            var post = new HNPost { Uri = "httppp:/notvalid" };
            var validator = new HNPostValidator(HNPage.BASE_URI);

            // ACT: run the validator on the post
            validator.ValidatePost(post);

            // ASSERT: the post should contain a validation error
            Assert.NotNull(post.ValidationErrors);
            Assert.True(post.ValidationErrors.Where(e => e.MemberNames.Contains("Uri")).Any());
        }
    }
}
