using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Utils.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils.Email
{
    public class EmailSender : IEmailSender
    {
        IConfiguration _config;
        private SendGridConfig _sendGrid;

        public EmailSender(IConfiguration config, IOptions<SendGridConfig> sendGrid)
        {
            _config = config;
            _sendGrid = sendGrid.Value;
        }
        public Task Execute(string ApiKey, string Subject, string Message, string Email)
        {
            var client = new SendGridClient(_sendGrid.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_sendGrid.Default_From_Email, _sendGrid.SendGridUser),
                Subject = Subject,
                PlainTextContent = Message,
                HtmlContent = Message

            };
            msg.AddTo(new EmailAddress(Email));

            //disable click tracking
            //see 
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);

        }

        public Task SendContactEmail(ContactUsModel contact)
        {
            return Execute(_sendGrid.SendGridKey, contact.SubjectTitle, contact.MessageBody, _sendGrid.ContactEmail);
        }

        public Task SendEmailAsync(string Email, string Subject, string Message)
        {
            return Execute(_sendGrid.SendGridKey, Subject, Message, Email);
        }

        public Task SendPasswordResetEmail(User user, string Origin)
        {
            string Message;
            if (!string.IsNullOrEmpty(Origin))
            {
                Uri ResetUrl = new Uri($"{Origin}/api/v1/users/reset-password?token={user.ResetToken}");
                Message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{ResetUrl}"">{ResetUrl}</a></p>";
            }
            else
            {
                Message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{user.ResetToken}</code></p>";
            }

            return Execute(_sendGrid.SendGridKey, $"{_sendGrid.CompanyName} User Verification API - Reset Password", Message, user.Email);
            //_emailService.Send(
            //    to: account.Email,
            //    subject: "Sign-up Verification API - Reset Password",
            //    html: $@"<h4>Reset Password Email</h4>
            //             {message}"
            //);
        }

        public Task SendVerificationEmail(User user, string Origin)
        {

            string Message;
            if (!string.IsNullOrEmpty(Origin))
            {
                Uri VerifyUrl = new Uri($"{Origin}/api/v1/users/verify-email?token={user.VerificationToken}");
                Message = $@"<p>Dear {user.FirstName},</p><p>Please click the below link to verify your email address:</p>
                             <p><a href=""{VerifyUrl}"">{VerifyUrl}</a></p>";
            }
            else
            {
                Message = $@"<p>Dear {user.FirstName},</p><p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{user.VerificationToken}</code></p>";
            }

            return Execute(_sendGrid.SendGridKey, $"{_sendGrid.CompanyName} - Account Verification", Message, user.Email);

            //_emailService.Send(
            //    to: account.Email,
            //    subject: "Sign-up Verification API - Verify Email",
            //    html: $@"<h4>Verify Email</h4>
            //             <p>Thanks for registering!</p>
            //             {message}"
            //);


        }
    }
}
