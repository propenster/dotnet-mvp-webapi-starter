using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Models.Stripe;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using DotNetWebAPIMVPStarter.Utils.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using SessionLineItemOptions = Stripe.Checkout.SessionLineItemOptions;
using SessionLineItemPriceDataOptions = Stripe.Checkout.SessionLineItemPriceDataOptions;
using SessionLineItemPriceDataProductDataOptions = Stripe.Checkout.SessionLineItemPriceDataProductDataOptions;

namespace DotNetWebAPIMVPStarter.Services.Implementations
{
    public class StripeService : IStripeService
    {
        private StripeConfig _settings;

        public StripeService(IOptions<StripeConfig> settings)
        {
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.ApiKey;  
        }

        public Response<Stripe.Customer> CreateNewStripeCustomer(CreateCustomerRequest request)
        {
            Stripe.Customer stripeCreateCustomerResponse;
            var options = new CustomerCreateOptions
            {
                Address = new AddressOptions
                {
                    City = request.Address.City,
                    Country = request.Address.Country,
                    Line1 = request.Address.Line1,
                    Line2 = request.Address.Line2,
                    PostalCode = request.Address.PostalZipCode,
                    State = request.Address.StateRegion
                },
                Description = "This is my very first Created Customer on Stripe in .NET Core",
                Email = request.Email,
                Name = $"{request.Name}",
                Phone = request.Phone,
                Shipping = new ShippingOptions
                {
                    Address = new AddressOptions
                    {
                        City = request.Shipping.Address.City,
                        Country = request.Shipping.Address.Country,
                        Line1 = request.Shipping.Address.Line1,
                        Line2 = request.Shipping.Address.Line2,
                        PostalCode = request.Shipping.Address.PostalZipCode,
                        State = request.Shipping.Address.StateRegion
                    },
                    Name = $"{request.Name}",
                    Phone = request.Phone
                }

            };
            var service = new CustomerService();
            stripeCreateCustomerResponse = service.Create(options);

            return new Response<Stripe.Customer>(stripeCreateCustomerResponse);

        }

        public Response<string> CreateStripeCheckoutSession(SessionCreatOptions model)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
          "card",
        },
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return new Response<string>(session.Id);
        }

        public Response<object> CreateStripeCheckoutSessionV2(SessionCreatOptions model)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
          "card",
        },
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return new Response<object>(session);
        }

        public Response<object> CreateSubscription(CreateSubscriptionRequest request)
        {
            var options = new SubscriptionCreateOptions
            {
                Customer = request.Customer,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = request.PriceId,
                    }
                }
            };

            var service = new SubscriptionService();
            object response = service.Create(options);

            return new Response<object>(response);
        }

        public Response<object> RetrieveASubscription(string SubscriptionId)
        {
            var service = new SubscriptionService();
            var response = service.Get(SubscriptionId);
            return new Response<object>(response);
        }
    }
}
