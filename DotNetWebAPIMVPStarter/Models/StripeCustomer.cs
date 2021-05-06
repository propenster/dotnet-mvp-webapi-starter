using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models
{
    [Table("StripCustomers")]
    public class StripeCustomer
    {
        [Key]
        public int Id { get; set; }
        public string StripeCustomerId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


    }
}
