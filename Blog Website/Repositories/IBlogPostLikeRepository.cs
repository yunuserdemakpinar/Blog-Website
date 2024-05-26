using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task AddAsync(BlogPostLike blogPostLike);
        Task<IEnumerable<BlogPostLike>> GetAllLikesForPost(Guid postId);
    }
}
