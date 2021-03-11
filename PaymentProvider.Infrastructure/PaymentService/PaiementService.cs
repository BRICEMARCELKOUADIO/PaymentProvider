using PaymentProvider.Domains.PaiementAggregate.Abstrations;
using PaymentProvider.Domains.PaiementAggregate.Models;
using PaymentProvider.Domains.Enums;
using PaymentProvider.Domains.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentProvider.Infrastructure.Dto;

namespace PaymentProvider.Infrastructure.PaymentService
{
    public class PaiementService : IPaiement
    {
        private readonly PaiementOptions _paiementOptions;

        public PaiementService(IOptions<PaiementOptions> paiementOptions)
        {
            _paiementOptions = paiementOptions.Value ?? throw new ArgumentNullException(nameof(paiementOptions));
        }

        public Task<PaiementResult<string>> GetPaiementUrl(decimal amount, int transactionId, string signature)
        {
            throw new Exception();
        }

        public async Task<PaiementResult<string>> GetSignature(decimal amount, int transactionId)
        {
            try
            {
                var client = new HttpClient();

                var paiementData = new List<KeyValuePair<string, string>>();

                paiementData.Add(new KeyValuePair<string, string>("cpm_amount", amount.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_currency", _paiementOptions.Currency.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_site_id", _paiementOptions.SiteID));
                paiementData.Add(new KeyValuePair<string, string>("cpm_trans_id", transactionId.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_trans_date", DateTime.Now.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_payment_config", _paiementOptions.PaymentConfiguration));
                paiementData.Add(new KeyValuePair<string, string>("cpm_page_action", _paiementOptions.PageAction));
                paiementData.Add(new KeyValuePair<string, string>("cpm_version", _paiementOptions.ApiVersion));
                paiementData.Add(new KeyValuePair<string, string>("cpm_language", _paiementOptions.Language));
                paiementData.Add(new KeyValuePair<string, string>("cpm_designation", "Paiement Rotary"));
                paiementData.Add(new KeyValuePair<string, string>("apikey", _paiementOptions.ApiKey));

                var req = new HttpRequestMessage(HttpMethod.Post, _paiementOptions.SignatureUrl) { Content = new FormUrlEncodedContent(paiementData) };
                var res = await client.SendAsync(req);

                if (res.IsSuccessStatusCode)
                {
                    var response = await res.Content.ReadAsStringAsync();
                    StatutSignatureDto result = new StatutSignatureDto();

                    if (response.Contains("status"))
                        result = JsonConvert.DeserializeObject<StatutSignatureDto>(response);
                    else
                        result.status = new StatutSignatureDetailDto() { code = "00", message = response };

                    return new PaiementResult<string>() 
                    { 
                        ResultCode = (result.status.code != "00") ? StatusCode.Failed : StatusCode.Success , Response = result.status.message 
                    };
                }
                else
                {
                    return new PaiementResult<string>() { ResultCode = StatusCode.Failed,};
                }
            }
            catch (Exception ex)
            {
                return new PaiementResult<string>() { ResultCode = StatusCode.Failed, Message = ex.Message };
            }
        }

        public Task<PaiementResult<string>> GetPaiementInfos(int transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
