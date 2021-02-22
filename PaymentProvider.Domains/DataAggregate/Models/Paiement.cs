using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Domains.DataAggregate.Models
{
    public class Paiement
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string SiteID { get; set; }
        public string ApiKey { get; set; }
        public string TransactionId { get; set; }
        public DateTime TansactionDate { get; set; }
        public string PaymentConfiguration { get; set; }
        public string PageAction { get; set; }
        public string Language { get; set; }
    }
}
