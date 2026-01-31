using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserManagementController : Controller
    {
        IUserManagementService userManagementService;

        public UserManagementController(IUserManagementService userManagementService) 
        {
            this.userManagementService = userManagementService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> registerUser(RegisterUserRequest registerUserRequest) 
        {
            try
            {
                var result = await userManagementService.RegisterUser(registerUserRequest);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var result = await userManagementService.Login(loginRequestDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{UserId}/getuser")]
        public async Task<IActionResult> GetUserById(Guid UserId)
        {
            try
            {
                var result = await userManagementService.GetUserById(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //public IActionResult Index()
        //{
        //    return Ok("User management API is running");
        //}
    }
}
