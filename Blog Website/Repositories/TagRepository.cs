using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Blog_Website.Models.View;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext bloggieDbContext;

        public TagRepository(BlogDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag != null)
                return tag;
            else
                return null;
        }

        public async Task<bool> UpdateAsync(Tag editTagRequest)
        {
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == editTagRequest.Id);

            if (tag != null)
            {
                tag.Name = editTagRequest.Name;
                tag.DisplayName = editTagRequest.DisplayName;

                await bloggieDbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
