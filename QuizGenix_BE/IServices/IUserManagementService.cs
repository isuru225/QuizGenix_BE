using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface IUserManagementService
    {
        public Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest);
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<UserInfoDto> GetUserById(Guid UserId);
        public Task<List<UserInfoDto>> GetAllStudents(Guid TeacherId);
    }
}
