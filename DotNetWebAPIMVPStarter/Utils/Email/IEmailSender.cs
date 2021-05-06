using DotNetWebAPIMVPStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string Email, string Subject, string Message);
        Task Execute(string ApiKey, string Subject, string Message, string Email);
        Task SendVerificationEmail(User User, string Origin);
        Task SendContactEmail(ContactUsModel contact);
        Task SendPasswordResetEmail(User User, string Origin);
    }
}
