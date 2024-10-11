using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

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
                var author = new Author
                {
                    Id = user.Id,
                    Name = registerUser.Name,
                };

                _authorServices.RegisterAuthor(author);

                return Ok(await GerarJwt(registerUser.Email));
            }


            return Problem("Failed to register user");
        }

        [HttpPost("login")]        
        public async Task<ActionResult> Login(LoginUser loginUser)
        {

            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return  Ok(await GerarJwt(loginUser.Email));
            }

            return Problem("Incorrect username or password");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtToken.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtToken.Emissor,
                Audience = _jwtToken.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtToken.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
