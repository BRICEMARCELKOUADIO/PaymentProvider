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

namespace PaymentProvider.Infrastructure.PaymentService
{
    public class PaiementService : IPaiement
    {
        private readonly PaiementOptions _paiementOptions;

        public PaiementService(IOptions<PaiementOptions> paiementOptions)
        {
            _paiementOptions = paiementOptions.Value ?? throw new ArgumentNullException(nameof(paiementOptions));
        }

        public PaiementResult<string> GetPaiementUrl(decimal amount, int transactionId, string signature, string userId)
        {
            try
            {
                if (amount < 100 || transactionId <= 0 || string.IsNullOrEmpty(signature))
                {
                    return new PaiementResult<string>()
                    {
                        ResultCode = StatusCode.Failed, Response = "MINIMUM REQUIRED FIELDS"
                    };
                }

                var url = $"{_paiementOptions.PaiementUrl}?cpm_amount={amount.ToString()}&cpm_currency={_paiementOptions.Currency.ToString()}&cpm_site_id={_paiementOptions.SiteID}";
                url += $"&cpm_trans_id={transactionId.ToString()}&cpm_trans_date={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}&cpm_payment_config={_paiementOptions.PaymentConfiguration}";
                url += $"&cpm_page_action={_paiementOptions.PageAction}&cpm_version={_paiementOptions.ApiVersion}&cpm_language={_paiementOptions.Language}";
                url += $"&cpm_designation=Paiement Rotary&cpm_custom={userId}&apikey={_paiementOptions.ApiKey}";
                url += $"&signature={signature.Replace("\"", "")}&notify_url={_paiementOptions.NotificationUrl}";

                return new PaiementResult<string>()
                {
                    ResultCode = StatusCode.Success, Response = url
                };
            }
            catch (Exception ex)
            {
                return new PaiementResult<string>() { ResultCode = StatusCode.Failed, Message = ex.Message };
            }
        }

        public async Task<PaiementResult<string>> GetSignature(decimal amount, int transactionId)
        {
            try
            {

                if (amount < 100 || transactionId <= 0)
                {
                    return new PaiementResult<string>()
                    {
                        ResultCode = StatusCode.Failed,
                        Response = "MINIMUM REQUIRED FIELDS"
                    };
                }

                var client = new HttpClient();

                var paiementData = new List<KeyValuePair<string, string>>();

                paiementData.Add(new KeyValuePair<string, string>("cpm_amount", amount.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_currency", _paiementOptions.Currency.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_site_id", _paiementOptions.SiteID));
                paiementData.Add(new KeyValuePair<string, string>("cpm_trans_id", transactionId.ToString()));
                paiementData.Add(new KeyValuePair<string, string>("cpm_trans_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
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
                    StatutSignature result = new StatutSignature();

                    if (response.Contains("status"))
                        result = JsonConvert.DeserializeObject<StatutSignature>(response);
                    else
                        result.status = new StatutSignatureDetail() { code = "00", message = response };

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

        public async Task<PaiementResult<StatutPayment>> GetPaiementInfos(int transactionId)
        {
            try
            {
                var client = new HttpClient();

                var paiementData = new List<KeyValuePair<string, string>>();

                paiementData.Add(new KeyValuePair<string, string>("apikey", _paiementOptions.ApiKey));
                paiementData.Add(new KeyValuePair<string, string>("cpm_site_id", _paiementOptions.SiteID));
                paiementData.Add(new KeyValuePair<string, string>("cpm_trans_id", transactionId.ToString()));                

                var req = new HttpRequestMessage(HttpMethod.Post, _paiementOptions.PaiementInfosUrl) { Content = new FormUrlEncodedContent(paiementData) };
                var res = await client.SendAsync(req);

                if (res.IsSuccessStatusCode)
                {
                    var response = await res.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<StatutPayment>(response);

                    return new PaiementResult<StatutPayment>()
                    {
                        ResultCode = (result != null && result?.Transaction.cpm_result == "00") ? StatusCode.Success : StatusCode.Failed, Response = result
                    };
                }
                else
                {
                    return new PaiementResult<StatutPayment>() { ResultCode = StatusCode.Failed, };
                }
            }
            catch (Exception ex)
            {
                return new PaiementResult<StatutPayment>() { ResultCode = StatusCode.Failed, Message = ex.Message };
            }
        }
    }
}
