using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Stripe
{
    public class SessionCreatOptions
    {
        public List<string> PaymentMethodTypes { get; set; }
        public List<SessionLineItemOptions> LineItems { get; set; }
        public string Mode { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }


    }

    public class SessionLineItemOptions
    {
        public SessionLineItemPriceDataOptions PriceData { get; set; }
        public int Quantity { get; set; }
    }

    public class SessionLineItemPriceDataOptions
    {
        public int UnitAmount { get; set; }
        public string Currency { get; set; } //usd ngn
        public SessionLineItemPriceDataProductDataOptions ProductData { get; set; }

    }

    public class SessionLineItemPriceDataProductDataOptions
    {
        public string Name { get; set; } //e.g T-shirt
    }
}
