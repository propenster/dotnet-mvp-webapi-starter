using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models
{
    public class ContactUsModel
    {
        //[Key]
        public int Id { get; set; }
        public string SubjectTitle { get; set; }
        public string CompanyEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string JobTitle { get; set; }
        public string ReasonForContact { get; set; }
        public string Company { get; set; }
        public string MessageBody { get; set; }
    }
}
