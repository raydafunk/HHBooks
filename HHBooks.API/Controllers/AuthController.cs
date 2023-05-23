using AutoMapper;
using HHBooks.API.Data;
using HHBooks.API.Modles.User;
using HHBooks.API.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HHBooks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Resigter(UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for{userDto.Email}");
            var user = _mapper.Map<ApiUser>(userDto);
             user.UserName = userDto.Email;
             var result = await _userManager.CreateAsync(user, userDto.Password!);

            try
            {
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, "User");
                return Accepted();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(Resigter)}");
                return Problem($"Something went wrong in the {nameof(Resigter)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> login(logiUserDto loginUserDto)
        {
            _logger.LogInformation($"login Attempt for {loginUserDto.Email}");
            try
            {
                var user =  await _userManager.FindByEmailAsync(loginUserDto.Email!);
                var passwordVaild = await _userManager.CheckPasswordAsync(user!, loginUserDto.Password!);
                if (user == null || passwordVaild == false)
                {
                    return Unauthorized(loginUserDto);
                }

                string tokenString = await GenerateWebToken(user);

                var response = new AuthResponse
                {
                    Email = loginUserDto.Email,
                    Token = tokenString,
                    UserId = user.Id,
                };

                return Accepted(response);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(Resigter)}");
                return Problem($"Something went wrong in the {nameof(Resigter)}", statusCode: 500);
            }
        }

        /// <summary>
        /// A method to get Generate a Web Token for the JWT 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        private async Task<string> GenerateWebToken(ApiUser user)
        {
            var seruitityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]!));
            var credentails = new SigningCredentials(seruitityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(CustomClaimTypes.Uid, user.Id),
            }
            .Union(rolesClaims)
            .Union(rolesClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"]!,
                audience: _configuration["JwtSettings:Audience"]!,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
                signingCredentials: credentails
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
