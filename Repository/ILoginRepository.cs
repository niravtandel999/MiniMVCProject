using MVCTaskProject.Models;

namespace MVCTaskProject.Repository
{
    public interface ILoginRepository
    {
        bool RegisterUser(RegisterDto registerDto);
        bool LoginUser(UserDto user);
        User GetUserByEmail(string email);
        bool VerifyOtp(string email, string otpInput);
        void UpdateUser(User user);
        string GenerateOtp(string email);
        bool ChangePassword(ChangePasswordDto changePassword);
    }
}
