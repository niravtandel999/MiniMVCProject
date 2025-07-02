using MVCTaskProject.Models;
using System;
using System.Linq;

namespace MVCTaskProject.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(RegisterDto registerDto)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == registerDto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var record = new User()
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
            };
            _context.Users.Add(record);
            var result = _context.SaveChanges();
            return result > 0; 
        }

        public bool LoginUser(UserDto user)
        {
            var record = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            return record != null && record.Password == user.Password;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString(); 
            var user = GetUserByEmail(email);

            if (user != null)
            {
                user.OTP = otp; 
                UpdateUser(user);
                return otp;
            }

            return null;
        }

        public bool VerifyOtp(string email, string otpInput)
        {
            var user = GetUserByEmail(email);
            return user != null && user.OTP == otpInput;
        }


        public bool ChangePassword(ChangePasswordDto changePassword)
        {
            var record = _context.Users.Select(x => x.Email == changePassword.CurrentPassword).FirstOrDefault();

            if(record == null)
            {
                return false;
            }
            else
            {
                var result = _context.Users.FirstOrDefault(x => x.Password == changePassword.CurrentPassword);

                if(result == null)
                {
                    return false;
                }
                else
                {
                    result.Password = changePassword.NewPassword;
                    _context.SaveChanges();
                    return true;
                }
            }
        }

    }
}
