using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Stripe
{
    public class CreateCustomerResponse
    {

        public string id { get; set; }
        public string @object { get; set; }
        public object address { get; set; }
        public int balance { get; set; }
        public int created { get; set; }
        public string currency { get; set; }
        public object default_source { get; set; }
        public bool delinquent { get; set; }
        public string description { get; set; }
        public object discount { get; set; }
        public object email { get; set; }
        public string invoice_prefix { get; set; }
        public InvoiceSettings invoice_settings { get; set; }
        public bool livemode { get; set; }
        public Metadata metadata { get; set; }
        public object name { get; set; }
        public int next_invoice_sequence { get; set; }
        public object phone { get; set; }
        public List<object> preferred_locales { get; set; }
        public object shipping { get; set; }
        public string tax_exempt { get; set; }
    }


    public class InvoiceSettings
    {
        public object custom_fields { get; set; }
        public object default_payment_method { get; set; }
        public object footer { get; set; }
    }

    public class Metadata
    {
    }



}
