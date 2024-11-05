using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogSimplesMvc.UI.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostServices _postServices;
        private readonly IAuthorServices _authorServices;

        public PostController(ILogger<PostController> logger, IPostServices postServices, IAuthorServices authorServices)
        {
            _logger = logger;
            _postServices = postServices;
            _authorServices = authorServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = _authorServices.GetAuthor(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id;
                _postServices.Insert(post);
                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        public IActionResult Edit(int id)
        {
            var post = _postServices.GetById(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id, Title, Description")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = _authorServices.GetAuthor(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id;
                _postServices.Update(post);
                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        public IActionResult Delete(int id)
        {
            _postServices.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
