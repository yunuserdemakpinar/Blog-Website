using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task AddAsync(BlogPostLike blogPostLike)
        {
            await bloggieDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostLike>> GetAllLikesForPost(Guid postId)
        {
            return await bloggieDbContext.BlogPostLikes.Where(x => x.BlogPostId == postId).ToListAsync();
        }
    }
}
