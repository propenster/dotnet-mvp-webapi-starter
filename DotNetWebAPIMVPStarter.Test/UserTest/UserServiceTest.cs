using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.Role;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace DotNetWebAPIMVPStarter.Test.UserTest
{
    public class UserServiceTest
    {

        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userService = new UserServiceMock();
        }

        [Fact]
        public void Test_Authenticate_Success_Returns_User_Type()
        {
            //Arrange
            //PasswordHash = Encoding.UTF8.GetBytes("eyjdhej.3hsgehg")
            string Email = "faitholusegun487@gmail.com";
            string Password = "eyjdhej3hsgehgia";
            //Act
            
            User AuthenticateUserResult = _userService.Authenticate(Email, Password); //this is a valid user I guess....

            //Assert
            Assert.IsType<User>(AuthenticateUserResult);

        }

        [Fact]
        public void Test_Authenticate_Success_Returns_The_Right_Authenticated_User()
        {
            //Arrange
            //PasswordHash = Encoding.UTF8.GetBytes("eyjdhej.3hsgehg")
            string Email = "faitholusegun487@gmail.com";
            string Password = "eyjdhej3hsgehgia";
            //Act

            User AuthenticateUserResult = _userService.Authenticate(Email, Password); //this is a valid user I guess....

            //Assert
            var Item = Assert.IsType<User>(AuthenticateUserResult);
            Assert.Equal("lagos", Item.City.ToLower());

        }

        [Fact]
        public void Test_Create_Admin_User_Returns_A_User_Type()
        {
            //Arrange
            User UserToBeCreated = new User { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "jason@bourneconspiracy.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateAdminUser(UserToBeCreated, Password, RepeatPassword, Role.Admin);

            //Assert
            Assert.IsType<User>(Result);
        }

        [Fact]
        public void Test_Create_Admin_User_With_Already_Existing_Email_Returns_Null()
        {
            //Arrange
            User UserToBeCreated = new User { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "faitholusegun487@gmail.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateAdminUser(UserToBeCreated, Password, RepeatPassword, Role.Admin);

            //Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Test_Create_Admin_User_Creates_An_Item_Successfully()
        {
            //Arrange
            User UserToBeCreated = new User() { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "jason@conspiracy.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateAdminUser(UserToBeCreated, Password, RepeatPassword, Role.Admin);

            //Assert
            var Item = Assert.IsType<User>(Result);
            Assert.Equal(Role.Admin, Item.Role);
        }

        [Fact]
        public void Test_Create_Normal_User_Returns_A_User_Type()
        {
            //Arrange
            User UserToBeCreated = new User { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "jason@bourneconspiracy.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateUser(UserToBeCreated, Password, RepeatPassword);

            //Assert
            Assert.IsType<User>(Result);
        }

        [Fact]
        public void Test_Create_Normal_User_With_Already_Existing_Email_Returns_Null()
        {
            //Arrange
            User UserToBeCreated = new User { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "faitholusegun487@gmail.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateUser(UserToBeCreated, Password, RepeatPassword);

            //Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Test_Create_Normal_User_Creates_An_Item_Successfully()
        {
            //Arrange
            User UserToBeCreated = new User() { Id = 3, StripeCustomerId = "Cus_xteyxsg3fsg4874bdvbdfd", FirstName = "Json", LastName = "Bourne", OtherNames = "Ultimus", PhoneNumber = "+10909090909", Email = "jason@conspiracy.com", Username = "jsonbourne", AddressLine1 = "2, Kofo Abayomi Street,", AddressLine2 = "Victoria Island, Lagos", City = "Lagos", PostalZipCode = "100273", StateRegion = "Lagos", Country = "Nigeria", PasswordHash = Encoding.ASCII.GetBytes("eyjdhej3hsgehgia"), PasswordSalt = Encoding.ASCII.GetBytes("eyj4is42dhej3hsgehg"), DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now, VerificationToken = "efft3thsgyesha4hy23diw22ghs34j", DateVerified = null, AcceptedTerms = true, ResetToken = "efft3thsgyesha4hy23diw22ghs34j", ResetTokenExpires = null, PasswordReset = null, Role = Role.Admin }; //I removed IsVerified from props
            string Password = "eyjdhej3hsgehgia";
            string RepeatPassword = "eyjdhej3hsgehgia";
            //Act
            var Result = _userService.CreateUser(UserToBeCreated, Password, RepeatPassword);

            //Assert
            var Item = Assert.IsType<User>(Result);
            Assert.Equal(Role.User, Item.Role);
        }

        [Fact]
        public void Test_Get_All_Users_Returns_All_Users()
        {
            //Arrange

            //Act
            var Result = _userService.GetAllUsers();

            //Assert
            Assert.IsType<List<User>>(Result);
        }

        [Fact]
        public void Test_Get_All_Users_Returns_The_Right_Number_Of_Items()
        {
            //Arrange

            //Act
            var Result = _userService.GetAllUsers();

            //Assert
            var Items = Assert.IsType<List<User>>(Result);
            Assert.Equal(2, Items.Count);
        }

        [Fact]
        public void Test_Get_User_By_Valid_Id_Returns_User_Type()
        {
            //Arrange
            int Id = 1;
            //Act
            var Result = _userService.GetUserById(Id);

            //Assert
            Assert.IsType<User>(Result);
        }
        [Fact]
        public void Test_Get_User_By_Valid_Id_Returns_The_Right_User()
        {
            //Arrange
            int Id = 1;
            //Act
            var Result = _userService.GetUserById(Id);

            //Assert
            var Item = Assert.IsType<User>(Result);
            Assert.Equal("efft3thsgyesha4hy23diw22ghs34j", Item.ResetToken);
        }

        [Fact]
        public void Test_Get_User_By_InValid_Id_Returns_Null()
        {
            //Arrange
            int Id = 1000; //invalid, unexisting Id
            //Act
            var Result = _userService.GetUserById(Id);

            //Assert
            Assert.Null(Result);
        }

        //[Fact]
        //public void Test_Delete_Unexisting_User_Returns_Null()
        //{
        //    //Arrange
        //    int Id = 1000;
        //    //Act
        //    Assert.Null(_userService.DeleteUser(Id));
        //    //Assert
        //}
    }
}
