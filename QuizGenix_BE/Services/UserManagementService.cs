using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using QuizGenix_BE.DataLayer;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.Services
{
    public class UserManagementService : IUserManagementService
    {
        private QuizGenixDBContext quizGenixDBContext;
        ILogger<UserManagementService> logger;
        ITokenService tokenService;
        public UserManagementService(QuizGenixDBContext quizGenixDBContext, ILogger<UserManagementService> logger, ITokenService tokenService)
        {
            this.quizGenixDBContext = quizGenixDBContext;
            this.logger = logger;
            this.tokenService = tokenService;
        }


        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {

            Boolean isEmailAlreadyExists = await quizGenixDBContext.Users.AnyAsync(user => user.Email == registerUserRequest.Email);
            Boolean isUserNameAlreadyExists = await quizGenixDBContext.Users.AnyAsync(user => user.Username == registerUserRequest.Username);

            if (isEmailAlreadyExists)
            {
                throw new Exception("Email already exists");
            }

            if (isUserNameAlreadyExists)
            {
                throw new Exception("User name is already exists");
            }

            User user = new User
            {
                Username = registerUserRequest.Username,
                Email = registerUserRequest.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password),
                Role = registerUserRequest.Role,
                Grade = registerUserRequest.Grade
            };

            quizGenixDBContext.Users.Add(user);
            await quizGenixDBContext.SaveChangesAsync();

            RegisterUserResponse registerUserResponse = new RegisterUserResponse
            {
                Token = tokenService.GenerateJwtToken(user),
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return registerUserResponse;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await quizGenixDBContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            return new LoginResponseDTO
            {
                Token = tokenService.GenerateJwtToken(user),
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<UserInfoDto> GetUserById(Guid UserId) 
        {
            var result = await quizGenixDBContext.Users.FirstOrDefaultAsync(user => user.Id == UserId);
            if (result == null) 
            {
                throw new Exception("User not found");
            }

            return new UserInfoDto
            {
                Id = result.Id,
                Username = result.Username,
                Email = result.Email,
                Role = result.Role,
                Grade = result.Grade,
                AdmissionDate = result.AdmissionDate
            };
            
        }
    }
}
