using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddAsync(BlogPostComment comment)
        {
            await bloggieDbContext.BlogPostComments.AddAsync(comment);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllByPostIdAsync(Guid postId)
        {
            return await bloggieDbContext.BlogPostComments.Where(x => x.BlogPostId == postId).ToListAsync();
        }
    }
}
