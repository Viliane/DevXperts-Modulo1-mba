using BlogSimpleCore.Helper;
using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;
using BlogSimplesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BlogSimplesAPI.Controllers
{
    [Authorize()]
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostServices _postServices;

        public PostController(ILogger<PostController> logger, IPostServices postServices)
        {
            _logger = logger;
            _postServices = postServices;
        }
                
        [HttpGet()]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            return Ok(_postServices.GetAll());
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            return Ok(_postServices.GetById(id));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(PostView postView)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var post = new Post
            {
                Title = postView.Title,
                Description = postView.Description,
                PublicationDate = postView.PublicationDate,
                AuthorId = postView.AuthorId
            };

            if (!Validate.IsValidateUser(HttpContext.User, post.AuthorId))
            {
                return new ObjectResult("You are not allowed to insert the post.") { StatusCode = 403 };
            }

            _postServices.Insert(post);
            return CreatedAtAction("Post", new { id = post.Id }, post);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public IActionResult Put(int id, PostView postView)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var post = new Post
            {
                Id = id,
                Title = postView.Title,
                Description = postView.Description,
                PublicationDate = postView.PublicationDate,
                AuthorId = postView.AuthorId
            };

            if (!Validate.IsValidateUser(HttpContext.User, post.AuthorId))
            {
                return new ObjectResult("You are not allowed to edit the post.") { StatusCode = 403 };
            }

            _postServices.Update(post);

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
                return new ObjectResult("You are not allowed to delete the post.") { StatusCode = 403 };
            }

            _postServices.Delete(id);
            return NoContent();
        }
    }
}
