using BlogSimpleCore.Services.Interfaces;
using BlogSimplesMvc.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimplesMvc.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostServices _postServices;

        public HomeController(ILogger<HomeController> logger, IPostServices postServices)
        {
            _logger = logger;
            _postServices = postServices;
        }

        public IActionResult Index()
        {   
            PostViewModel posts = new PostViewModel() { posts = _postServices.GetAll() };
            
            return View(posts);   
        }
    }
}
