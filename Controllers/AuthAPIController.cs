using GithubRepoApi.Models.DTO;
using GithubRepoApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GithubRepoApi.Controllers
{
    /// <summary>
    /// AuthController that has two methods
    /// 1 - Register for new user registration
    /// 2 - Login for new user login (with JWT)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        /// <summary>
        /// Register method that recieves new RegistrationRequestDto object and returns http OK if success
        /// otherwise BadRequest
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ResponseDto> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return _response;
            }
            return _response;
        }

        /// <summary>
        /// Login method. Executed when user logins to the system.
        /// Recieves LoginRequestDto object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ResponseDto> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
        
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password incorrect";
                return _response;
            }
        
            _response.Result = loginResponse;
            return _response;
        }
    }
}
