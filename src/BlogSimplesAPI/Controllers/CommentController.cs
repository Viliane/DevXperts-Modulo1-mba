using BlogSimpleCore.Helper;
using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;
using BlogSimplesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography;

namespace BlogSimplesAPI.Controllers
{
    [Authorize()]
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentServices _commentServices;

        public CommentController(ILogger<CommentController> logger, ICommentServices commentServices)
        {
            _logger = logger;
            _commentServices = commentServices;
        }
                
        [HttpGet()]
        [ProducesResponseType(typeof(Comments), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            return Ok(_commentServices.GetAll());
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Comments), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            
            return Ok(_commentServices.GetById(id));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Insert(CommentView commentsView)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var comments = new Comments
            {   
                PostId = commentsView.PostId,
                Description = commentsView.Description
            };

            if (!Validate.IsValidateUser(HttpContext.User, commentsView.AuthorId))
            {
                return new ObjectResult("You are not allowed to edit the comment.") { StatusCode = 403 };
            }

            _commentServices.Insert(comments);
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, CommentView commentsView)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var comments = new Comments
            {
                Id = id,
                PostId = commentsView.PostId,
                Description = commentsView.Description
            };

            if (!Validate.IsValidateUser(HttpContext.User, commentsView.AuthorId))
            {
                return new ObjectResult("You are not allowed to edit the comment.") { StatusCode = 403 };
            }

            _commentServices.Update(comments);
            
            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id, string AuthorId)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }
             
            if (!Validate.IsValidateUser(HttpContext.User, AuthorId))
            {
                return new ObjectResult("You are not allowed to delete the comment.") { StatusCode = 403 };
            }   

            _commentServices.Delete(id);
            return NoContent();
        }
    }
}
