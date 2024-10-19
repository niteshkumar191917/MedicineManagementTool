using MedicineManagementTool.BAL.IAuthenticate;
using MedicineManagementTool.BAL.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManagementTool.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, IAuthenticateService authenticateService, IEmailService emailService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserDTO newUser)
        {
            if (await _userService.CreateAsync(newUser))
            {
                _emailService.SendEmail(newUser);
                return Ok(newUser);
            }

            return BadRequest("Something wrong");
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<AuthenticationResponseDTO> EmailAndPassword(LoginCredentialDTO loginCredential)
        {
            try
            {
                bool check = await _userService.CheckLoginAsync(loginCredential);
                if (!check)
                {
                    return new AuthenticationResponseDTO
                    {
                        IsAuthSuccessful = false,
                        Token = null,
                        UserName = null
                    };
                }
                return _authenticateService.Authenticate(loginCredential.Email, loginCredential.Password);
            }
            catch
            {
                return new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false
                };
            }
        }

        [AllowAnonymous]
        [HttpGet("GetUserId")]
        public async Task<ActionResult> GetUserId(string email)
        {
            try
            {
                var result = await _userService.UserIdByEmial(email);
                if (result == 0)
                {
                    return BadRequest("Something wrong");
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something wrong");
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ResponseDTO<UserDTO>>> GetAllUsers([FromQuery] PaginationDTO paginationDto, int sortCount, string sortColumn)
        {
            try
            {
                var userData = await _userService.GetAllAsync(paginationDto, sortCount, sortColumn);
                if (userData == null || userData.Count == 0)
                {
                    return new ResponseDTO<UserDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = ""
                    };
                }

                userData.StatusCode = 1;
                return userData;
            }
            catch
            {
                return new ResponseDTO<UserDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error"
                };
            }
        }

        [HttpGet("SearchUser")]
        public async Task<ActionResult<ResponseDTO<UserDTO>>> SearchUser([FromQuery] PaginationDTO paginationDto, string data)
        {
            try
            {
                var userData = await _userService.SearchAsync(paginationDto, data);
                if (userData == null || userData.Count == 0)
                {
                    return new ResponseDTO<UserDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = "Not Exist"
                    };
                }

                userData.StatusCode = 1;
                return userData;

            }
            catch
            {
                return new ResponseDTO<UserDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error"
                };
            }
        }
    }
}