using Blog_Website.Models;
using Blog_Website.Models.View;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await blogPostRepository.GetAllAsync();
            var tags = await tagRepository.GetAllAsync();

            var home = new Home
            {
                Posts = posts,
                Tags = tags
            };

            return View(home);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}