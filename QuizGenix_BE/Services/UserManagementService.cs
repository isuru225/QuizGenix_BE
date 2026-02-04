using Azure;
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
            var token = tokenService.GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Token = token,
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

        public async Task<List<UserInfoDto>> GetAllStudents(Guid TeacherId) 
        {
            //get teacher info based on teacher id
            var result = await quizGenixDBContext.Users.Include(i => i.Teachings).Where(e => e.Id == TeacherId).ToListAsync();
            //get student info 
            var students = await quizGenixDBContext.Users.Where(e => e.Role == UserRole.Student).ToListAsync();

            if (result == null || students == null)
            {
                throw new Exception("No students or teachers avaiable yet!");
            }

            List<int> grades = new List<int>(); // grades which are taught by the teacher
            foreach (var teaching in result[0].Teachings) 
            {
                grades.Add(teaching.Grade);
            } 

            List<UserInfoDto> userInfoDtos = new List<UserInfoDto>();
            foreach (var user in students) 
            {
                if (grades.Contains(user.Grade)) 
                {
                    userInfoDtos.Add(new UserInfoDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        Grade = user.Grade,
                        AdmissionDate = user.AdmissionDate,
                        Role = user.Role,
                    });
                }
            }

            return userInfoDtos;
        }
    }
}
