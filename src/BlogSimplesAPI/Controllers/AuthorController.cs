using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimplesAPI.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorServices _authorServices;

        public AuthorController(ILogger<AuthorController> logger, IAuthorServices authorServices)
        {
            _logger = logger;
            _authorServices = authorServices;
        }

        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            return Ok(_authorServices.GetAuthor(id));
        }

        [HttpPost()]
        public IActionResult Post(Author author)
        {
            _authorServices.RegisterAuthor(author);
            return CreatedAtAction("Author", new { id = author.Id }, author);
        }
    }
}
