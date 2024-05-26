using Blog_Website.Models.Domain;
using Blog_Website.Models.View;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var post = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var totalLike = await blogPostLikeRepository.GetTotalLikes(post.Id);
            var isLiked = false;

            if (signInManager.IsSignedIn(User))
            {
                var likesForBlog = await blogPostLikeRepository.GetAllLikesForPost(post.Id);
                var userId = userManager.GetUserId(User);

                if (userId != null)
                {
                    var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                    isLiked = likeFromUser != null;
                }
            }

            var blogComments = await blogPostCommentRepository.GetAllByPostIdAsync(post.Id);
            var comments = new List<BlogComment>();
            foreach (var comment in blogComments)
            {
                comments.Add(new BlogComment
                {
                    Description = comment.Description,
                    DateAdded = comment.DateAdded,
                    Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                });
            }

            var blog = new Blog
            {
                Id = post.Id,
                Heading = post.Heading,
                PageTitle = post.PageTitle,
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                FeaturedImageUrl = post.FeaturedImageUrl,
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate,
                Author = post.Author,
                Visible = post.Visible,
                Tags = post.Tags,
                TotalLikes = totalLike,
                IsLiked = isLiked,
                Comments = comments
            };

            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Blog blog)
        {
            if (signInManager.IsSignedIn(User))
            {
                var comment = new BlogPostComment
                {
                    BlogPostId = blog.Id,
                    Description = blog.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await blogPostCommentRepository.AddAsync(comment);

                return RedirectToAction("Index", "Blogs", new { urlHandle = blog.UrlHandle });
            }

            return View();
        }
    }
}
