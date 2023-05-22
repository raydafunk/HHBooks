using AutoMapper;
using HHBooks.API.Data;
using HHBooks.API.Modles.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HHBooks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
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
        public async Task<IActionResult> login(logiUserDto loginUserDto)
        {
            _logger.LogInformation($"login Attempt for {loginUserDto.Email}");
            try
            {
                var user =  await _userManager.FindByEmailAsync(loginUserDto.Email!);
                var passwordVaild = await _userManager.CheckPasswordAsync(user!, loginUserDto.Password!);
                if (user == null || passwordVaild == false)
                {
                    return NotFound();
                }   
                return Accepted(user);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(Resigter)}");
                return Problem($"Something went wrong in the {nameof(Resigter)}", statusCode: 500);
            }
        }
    }
}
