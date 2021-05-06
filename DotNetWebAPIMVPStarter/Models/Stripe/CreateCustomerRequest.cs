using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Stripe
{
    public class CreateCustomerRequest
    {
        public Address Address { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Shipping Shipping { get; set; }


    }

    public class Shipping
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; } //NG US
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalZipCode { get; set; }
        public string StateRegion { get; set; }

    }
}
