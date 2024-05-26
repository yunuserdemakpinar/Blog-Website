using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Tag editTagRequest);
        Task<bool> DeleteAsync(Guid id);
    }
}
