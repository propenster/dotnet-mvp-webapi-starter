using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.Role;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetWebAPIMVPStarter.Test.UserTest
{
    public class UserServiceMock : IUserService
    {

        List<User> _allUsers;

        public UserServiceMock()
        {

            _allUsers = new List<User>()
            {
                new User() { Id = 1, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Faith", LastName = "Olusegun", OtherNames = "Emmanuel", PhoneNumber = "+2348109354862", Email = "faitholusegun487@gmail.com", Username = "propenster", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null,  AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.User }, //I removed IsVerified from props

                new User() { Id = 2, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "John", LastName = "Smith", OtherNames = "", PhoneNumber = "+1809299292299", Email = "johnsmith@gmail.com", Username = "johnsmith", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "4, Foxtrot Street, Belfort, Michigan", City = "Lagos", PostalZipCode = "62352ghsf3", StateRegion = "Michigan", Country = "U.S.A", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null,  AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw2ertts34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.User }, //I removed IsVerified from props
            };

            //Encoding.UTF8.GetBytes("eyj4is42dhej3hsgehg"),
        }

        public User Authenticate(string Email, string Password)
        {
            var User = _allUsers.Where(x => x.Email == Email).FirstOrDefault();
            if (User == null) return null;
            byte[] PassHash = Encoding.ASCII.GetBytes(Password);
            for (int i = 0; i < User.PasswordHash.Length; i++)
            {
                if (PassHash[i] != User.PasswordHash[i]) return null;
            }
            //if (!PassHash.Equals(User.PasswordHash)) return null;


            //ok auth is successful!
            return User;
        }

        public User CreateAdminUser(User user, string Password, string RepeatPassword, string Role = "Admin")
        {
            //is User exisiting before...
            User CheckUser = _allUsers.Where(x => x.Email == user.Email).FirstOrDefault();
            if (CheckUser != null) return null;
            //are passwords equal?
            if (!Password.Equals(RepeatPassword)) return null;

            user.PasswordHash = Encoding.ASCII.GetBytes(Password);
            user.PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej.3hsgehg");
            user.Role = Role;

            _allUsers.Add(user);

            return user;
        }

        public User CreateUser(User user, string Password, string RepeatPassword)
        {
            //is User exisiting before...
            User CheckUser = _allUsers.Where(x => x.Email == user.Email).FirstOrDefault();
            if (CheckUser != null) return null;
            //are passwords equal?
            if (!Password.Equals(RepeatPassword, StringComparison.CurrentCulture)) return null;

            user.PasswordHash = Encoding.UTF8.GetBytes(Password);
            user.PasswordSalt = Encoding.UTF8.GetBytes("eyj4is42dhej.3hsgehg");
            user.Role = Role.User;

            _allUsers.Add(CheckUser);

            return user;
        }

        public void DeleteUser(int Id)
        {
            User CheckUser = _allUsers.First(x => x.Id == Id);
            if (CheckUser == null) return;
            _allUsers.Remove(CheckUser);
        }

        public void ForgotPassword(ForgotPasswordRequest model, string Origin)
        {
            Console.WriteLine("Forgot Password till later");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _allUsers;
        }

        public User GetUserById(int Id)
        {
            User CheckUser = _allUsers.Where(x => x.Id == Id).FirstOrDefault();
            if (CheckUser == null) return null;
            return CheckUser;
        }

        public void ResetPassword(ResetPasswordRequest model)
        {
            Console.WriteLine("What's Reset password");
        }

        public void UpdateUser(User user, string Password = null)
        {
            Console.WriteLine("Later Thanks!");
        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            Console.WriteLine("Later Thanks!");
        }

        public void VerifyEmail(string Token)
        {
            Console.WriteLine("Later Thanks!");

        }
    }
}
