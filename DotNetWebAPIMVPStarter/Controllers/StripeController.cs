using DotNetWebAPIMVPStarter.Models.Stripe;
using DotNetWebAPIMVPStarter.Services.Interfaces;
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
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }


        [HttpPost]
        [Route("create_stripe_session")]
        public IActionResult CreateStripeCheckoutSession([FromBody] SessionCreatOptions model)
        {
            return Ok(_stripeService.CreateStripeCheckoutSession(model));
        }

        [HttpPost]
        [Route("create_stripe_session_v2")]
        public IActionResult CreateStripeCheckoutSessionV2([FromBody] SessionCreatOptions model)
        {
            return Ok(_stripeService.CreateStripeCheckoutSessionV2(model));
        }

        [HttpPost]
        [Route("create_subscription")]
        public IActionResult CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            return Ok(_stripeService.CreateSubscription(request));
        }

        [HttpGet]
        [Route("retrieve_subscription")]
        public IActionResult RetrieveASubscription(string SubscriptionId)
        {
            return Ok(_stripeService.RetrieveASubscription(SubscriptionId));
        }

        [HttpPost("create_new_customer")]
        public IActionResult CreateNewCustomer([FromBody] CreateCustomerRequest request)
        {
            return Ok(_stripeService.CreateNewStripeCustomer(request));
        }

    }
}
