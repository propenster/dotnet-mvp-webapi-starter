using AutoMapper;
using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Models.Role;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using DotNetWebAPIMVPStarter.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        IMapper _mapper;
        private readonly IUserService _userService;
        IEmailSender _emailSender;

        public UsersController(IUserService userService, IMapper mapper, IEmailSender emailSender)
        {
            _userService = userService;
            _mapper = mapper;
            _emailSender = emailSender;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            User Account = _userService.Authenticate(model.Email, model.Password);
            if (Account == null) return BadRequest(new { Message = "Email or Password is incorrect" });
            string JwtToken = Utility.GenerateJwtToken(Account);

            return Ok(new
            {
                Email = Account.Email,
                Username = Account.Username,
                Token = JwtToken
            });
                
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = "Error in fields" });
            User Account = _mapper.Map<User>(model);
            string Origin = Request.Headers["Origin"];
            try
            {
                //create a new User
                _userService.CreateUser(Account, model.Password, model.RepeatPassword);
                _emailSender.SendVerificationEmail(Account, Origin);
                return Ok(new { ResponseCode = "200", ResponseMessage = "Check your email to confirm your registration" });

            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public IActionResult VerifyEmail(VerifyEmailRequest model)
        {
            _userService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }


        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequest model)
        {
            _userService.ForgotPassword(model, Request.Headers["Origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
        {
            _userService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest model)
        {
            _userService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            IEnumerable<User> Users = _userService.GetAllUsers();
            IList<UserModel> Model = _mapper.Map<IList<UserModel>>(Users);
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetUserById(int Id)
        {
            //only admins can check other users' info
            int CurrentUserId = int.Parse(User.Identity.Name);
            if (Id != CurrentUserId && !User.IsInRole(Role.Admin)) return Forbid();

            var user = _userService.GetUserById(Id);
            if (user == null) return NotFound();

            var model = _mapper.Map<UserModel>(user);

            return Ok(new Response<UserModel>(model));
            //return Ok(new Response { Errors = null, Message = "Successful", Succeeded = true, Data = user });
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int Id, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = "Invalid field values." });
            //map model to user
            User Account = _mapper.Map<User>(model);
            Account.Id = Id;
            try
            {
                //update user
                _userService.UpdateUser(Account, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                //return error message
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _userService.DeleteUser(Id);
            return Ok();
        }



    }
}
