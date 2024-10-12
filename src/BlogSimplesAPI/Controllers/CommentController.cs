using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            _commentServices.GetAll();
            return Ok();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            _commentServices.GetById(id);
            return Ok();

        }

        [HttpPost()]
        [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(Comments comments)
        {
            _commentServices.Insert(comments);
            return CreatedAtAction("Comment", new { id = comments.Id }, comments);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, Comments comments)
        {
            if (id != comments.Id) return BadRequest();
            _commentServices.Update(comments);
            
            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _commentServices.Delete(id);
            return NoContent();
        }
    }
}
