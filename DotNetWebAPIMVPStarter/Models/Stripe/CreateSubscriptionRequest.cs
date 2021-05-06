using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Stripe
{
    public class CreateSubscriptionRequest
    {
        public string Customer { get; set; } //CustomerId on Stripe
        public string PriceId { get; set; } //PriceId on Stripe
    }
}
