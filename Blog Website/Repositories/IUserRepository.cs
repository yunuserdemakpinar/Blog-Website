using Microsoft.AspNetCore.Identity;

namespace Blog_Website.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();
    }
}
