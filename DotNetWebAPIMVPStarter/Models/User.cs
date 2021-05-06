using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("stripe_customer_id")]
        public string? StripeCustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalZipCode { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; } //nationality
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        [JsonIgnore]
        public string VerificationToken { get; set; }
        public DateTime? DateVerified { get; set; }
        public bool IsVerified => DateVerified.HasValue;
        public bool AcceptedTerms { get; set; }
        [JsonIgnore]
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public string Role { get; set; }





    }
}
