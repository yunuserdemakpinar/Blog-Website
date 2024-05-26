using Azure;
using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Blog_Website.Models.View;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddAsync(BlogPost post)
        {
            await bloggieDbContext.BlogPosts.AddAsync(post);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            var post = await bloggieDbContext.BlogPosts.Include(x => x.Likes).Include(x => x.Tags).FirstOrDefaultAsync(t => t.Id == id);

            if (post != null)
                return post;
            else
                return null;
        }

        public async Task<bool> UpdateAsync(BlogPost editPostRequest)
        {
            var post = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(t => t.Id == editPostRequest.Id);

            if (post != null)
            {
                post.Heading = editPostRequest.Heading;
                post.PageTitle = editPostRequest.PageTitle;
                post.Content = editPostRequest.Content;
                post.ShortDescription = editPostRequest.ShortDescription;
                post.FeaturedImageUrl = editPostRequest.FeaturedImageUrl;
                post.UrlHandle = editPostRequest.UrlHandle;
                post.PublishedDate = editPostRequest.PublishedDate;
                post.Author = editPostRequest.Author;
                post.Visible = editPostRequest.Visible;
                post.Tags = editPostRequest.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await bloggieDbContext.BlogPosts.FirstOrDefaultAsync(t => t.Id == id);

            if (post != null)
            {
                bloggieDbContext.BlogPosts.Remove(post);
                await bloggieDbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<BlogPost> GetByUrlHandleAsync(string urlHandle)
        {
            var post = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(t => t.UrlHandle == urlHandle);

            if (post != null)
                return post;

            return null;
        }
    }
}
