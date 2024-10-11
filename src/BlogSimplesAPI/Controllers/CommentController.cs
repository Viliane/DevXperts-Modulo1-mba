using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimplesAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult GetAll()
        {
            _commentServices.GetAll();
            return Ok();
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            _commentServices.GetById(id);
            return Ok();

        }

        [HttpPost()]
        public IActionResult Post(Comments comments)
        {
            _commentServices.Insert(comments);
            return CreatedAtAction("Comment", new { id = comments.Id }, comments);
        }

        [HttpPut("id")]
        public IActionResult Put(int id, Comments comments)
        {
            if (id != comments.Id) return BadRequest();
            _commentServices.Update(comments);
            
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            _commentServices.Delete(id);
            return NoContent();
        }
    }
}
