using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task AddAsync(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetAllByPostIdAsync(Guid postId);
    }
}
