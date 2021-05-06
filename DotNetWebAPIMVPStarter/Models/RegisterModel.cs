using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; } //nationality
        [Required]
        [MinLength(6, ErrorMessage = "Password must be more than 6 characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string RepeatPassword { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        //public DateTime DateCreated { get; set; }
        //public DateTime DateLastUpdated { get; set; }
        //public string VerificationToken { get; set; }
        //public DateTime? DateVerified { get; set; }
        //public bool IsVerified => DateVerified.HasValue;
        public bool AcceptedTerms { get; set; }


    }
}
