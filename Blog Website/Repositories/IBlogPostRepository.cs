using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface IBlogPostRepository
    {
        Task AddAsync(BlogPost post);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(BlogPost post);
        Task<bool> DeleteAsync(Guid id);

        Task<BlogPost> GetByUrlHandleAsync(string urlHandle);
    }
}
