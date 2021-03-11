using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaymentProvider.Domains.Enums;
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

        [JsonProperty("SignatureUrl")]
        public string SignatureUrl { get; set; }

        [JsonProperty("PaymentConfiguration")]
        public string PaymentConfiguration { get; set; }

        [JsonProperty("PageAction")]
        public string PageAction { get; set; }

        [JsonProperty("PaiementInfosUrl")]
        public string PaiementInfosUrl { get; set; }

        [JsonProperty("PaiementUrl")]
        public string PaiementUrl { get; set; }

        [JsonProperty("NotificationUrl")]
        public string NotificationUrl { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }
        
        [JsonProperty("ApiVersion")]
        public string ApiVersion { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("PhonePrefix")]
        public string PhonePrefix { get; set; }

        public PaiementOptions Value => this;
    }
}
