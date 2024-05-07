using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Services;
using QuestionsAndAnswers.DataLayer.Models;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _authService;

        public AuthController(IUserService authService)
        {
            _authService = authService;
        }


        [HttpPost("authenticate")]
        public IActionResult  Authenticate(/*[FromBody]*/ AuthenticationRequest model)
        {
            var response = _authService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Неверное имя пользователя или пароль" });
            }

            return Ok(response);
            /*return Ok(new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.Patronymic,
                user.Email,
                user.Username,
                //user.Password
            });*/
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            //var response =  _authService.Register(model);
            var response = await _authService.Register(userModel);

            if (response == null)
            {
                return BadRequest(new { message = "Пользователь с таким логином уже существует" });
            }

            return Ok(response);
        }

        /*public IActionResult Register([FromBody] RegistrationRequest request)
        {
            var newUser = new User
            {
                //Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronumics,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };
            var user = _authService.Register(newUser, request.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Имя пользователя уже занято" });
            }

            return Ok(new
            {
                //user.Id,
                user.Name,
                user.Surname,
                user.Patronymic,
                user.Email,
                user.Username
            });
        }*/
    }
}
