using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserService _userService,
        IValidator<UserRegisterContract> _registerValidator,
        IValidator<UserLoginContract> _loginValidator,
        IMapper _mapper) : ControllerBase
    {
        [HttpGet("users")]
        public ActionResult<List<UserResponse>> GetAll(){

            var users = _userService.GetAllUsers();
            var response = _mapper.Map<List<UserResponse>>(users);


            return Ok(response);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserRegisterContract data)
        {
            var validationResult = _registerValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            if (_userService.EmailExists(data.Email))
                return Conflict("User with this email already exists.");

            var user = _mapper.Map<User>(data);
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, _mapper.Map<UserResponse>(user));
        }

        [HttpPost("login")]
        public ActionResult<UserResponse> Login([FromBody] UserLoginContract data)
        {
            var validationResult = _loginValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var user = _userService.GetUserByEmail(data.Email);
            if (user == null || user.PasswordHash != data.Password)
                return Unauthorized("Invalid email or password.");

            return Ok(_mapper.Map<UserResponse>(user));
        }

    }
}
