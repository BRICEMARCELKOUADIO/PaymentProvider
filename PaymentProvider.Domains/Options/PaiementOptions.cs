using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Domains.Options
{
    public class PaiementOptions : IOptions<PaiementOptions>
    {
        [JsonProperty("SiteID")]
        public string SiteID { get; set; }

        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("PaymentConfiguration")]
        public string PaymentConfiguration { get; set; }

        [JsonProperty("PageAction")]
        public string PageAction { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("PhonePrefix")]
        public string PhonePrefix { get; set; }

        public PaiementOptions Value => this;
    }
}
