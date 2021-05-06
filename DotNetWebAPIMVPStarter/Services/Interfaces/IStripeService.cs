using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Models.Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Interfaces
{
    public interface IStripeService
    {

        Response<string> CreateStripeCheckoutSession(SessionCreatOptions model);
        Response<object> CreateStripeCheckoutSessionV2(SessionCreatOptions model);
        Response<object> CreateSubscription(CreateSubscriptionRequest request);
        Response<object> RetrieveASubscription(string SubscriptionId);
        Response<Stripe.Customer> CreateNewStripeCustomer(CreateCustomerRequest request);
    }
}
