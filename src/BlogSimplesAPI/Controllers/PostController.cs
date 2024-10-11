using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimplesAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_postServices.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("id")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return Ok(_postServices.GetById(id));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(Post post)
        {
            _postServices.Insert(post);
            return CreatedAtAction("Post", new { id = post.Id }, post);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, Post post)
        {
            if (id != post.Id) return BadRequest();

            _postServices.Update(post);
            
            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _postServices.Delete(id);
            return NoContent();
        }
    }
}
