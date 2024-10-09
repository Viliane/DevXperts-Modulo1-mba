using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BlogSimplesAPI.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtToken _jwtToken;
        private readonly IAuthorServices _authorServices;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IAuthorServices authorServices,
            IOptions<JwtToken> jwtToken)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _authorServices = authorServices;
            _jwtToken = jwtToken.Value;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var signIn = _signInManager.SignInAsync(user, false);

                var author = new Author
                {
                    Id = signIn.Id.ToString(),
                    Name = registerUser.Name,
                };

                _authorServices.RegisterAuthor(author);

                return Ok(GerarJwt());
            }


            return Problem("Failed to register user");
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {

            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded) { 
                return Ok(GerarJwt());
            }

            return Problem("Incorrect username or password");
        }

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtToken.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtToken.Emissor,
                Audience = _jwtToken.Audiencia,
                //Subject = _jwtToken,
                Expires = DateTime.UtcNow.AddHours(_jwtToken.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
