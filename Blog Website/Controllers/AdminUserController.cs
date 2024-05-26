using Blog_Website.Models.View;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAllAsync();

            var usersModel = new Users();
            usersModel.User = new List<User>();

            foreach (var user in users)
            {
                usersModel.User.Add(new Models.View.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return View(usersModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(Users user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, user.Password);

            if (identityResult != null && identityResult.Succeeded)
            {
                var roles = new List<string> { "User" };
                if (user.AdminRoleCheckbox)
                    roles.Add("Admin");

                identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult != null && identityResult.Succeeded)
                    return RedirectToAction("List", "AdminUser");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult != null && identityResult.Succeeded)
                    return RedirectToAction("List", "AdminUser");
            }
            return View();
        }
    }
}
