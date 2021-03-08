using PaymentProvider.Domains.DataAggregate.Abstrations;
using PaymentProvider.Domains.DataAggregate.Models;
using PaymentProvider.Domains.Enums;
using PaymentProvider.Domains.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProvider.Infrastructure.PaymentService
{
    public class PaymentService : IPaiement
    {
        private readonly PaiementOptions _paiementOptions;

        public PaymentService(PaiementOptions paiementOptions)
        {
            _paiementOptions = _paiementOptions.Value ?? throw new ArgumentNullException(nameof(paiementOptions));
        }
        public Task<PaiementResult<string>> GetPaiementUrl(string PhoneNumber)
        {
            throw new Exception();
        }

        public async Task<PaiementResult<string>> GetSignature(string transactionId)
        {
            try
            {
                var client = new HttpClient();
                var paiementDatet = new List<KeyValuePair<string, string>>();
                paiementDatet.Add(new KeyValuePair<string, string>("apikey", _paiementOptions.ApiKey));
                paiementDatet.Add(new KeyValuePair<string, string>("cpm_site_id", _paiementOptions.SiteID));
                paiementDatet.Add(new KeyValuePair<string, string>("cpm_trans_id", transactionId));

                var req = new HttpRequestMessage(HttpMethod.Post, _paiementOptions.SignatureUrl) { Content = new FormUrlEncodedContent(paiementDatet) };

                var res = await client.SendAsync(req);

                if (res.IsSuccessStatusCode)
                {
                    //Deserialisation des données



                    return new PaiementResult<string>() { ResultCode = StatusCode.Success,Response = "" };
                }
                else
                {
                    return new PaiementResult<string>() { ResultCode = StatusCode.SignatureFailed };
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
