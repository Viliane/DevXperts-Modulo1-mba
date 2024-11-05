using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimplesMvc.UI.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        public readonly ICommentServices _commentsServices;

        public CommentController(ILogger<CommentController> logger, ICommentServices commentsServices)
        {
            _logger = logger;
            _commentsServices = commentsServices;
        }
        public IActionResult Index(int Id)
        {
            if (ModelState.IsValid)
            {
                Comments comments = new Comments() { PostId = Id };
                return View(comments);
            }

            return View();
        }

        public IActionResult Create(int Id)
        {
            if (ModelState.IsValid)
            {
                Comments comments = new Comments() { PostId = Id };
                return View(comments);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PostId, Description")] Comments comment)
        {
            if (ModelState.IsValid)
            {                
                _commentsServices.Insert(comment);
                return RedirectToAction("Index", "Home");
            }
            
            return View(comment);
        }

        public IActionResult Edit(int id)
        {
            var comment = _commentsServices.GetById(id);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id, PostId, Description")] Comments comment)
        {
            if (ModelState.IsValid)
            {
                _commentsServices.Update(comment);
                return RedirectToAction("Index", "Home");
            }

            return View(comment);
        }

        public IActionResult Delete(int id)
        {
            _commentsServices.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
