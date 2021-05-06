using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Interfaces
{
    public interface IUserService
    {

        User CreateUser(User user, string Password, string RepeatPassword);
        User CreateAdminUser(User user, string Password, string RepeatPassword, string Role = Role.Admin);
        IEnumerable<User> GetAllUsers();
        void UpdateUser(User user, string Password = null);
        void DeleteUser(int Id);
        User GetUserById(int Id);
        User Authenticate(string Email, string Password);
        void VerifyEmail(string Token);

        //
        void ForgotPassword(ForgotPasswordRequest model, string Origin);
        void ValidateResetToken(ValidateResetTokenRequest model);
        void ResetPassword(ResetPasswordRequest model);
    }
}
