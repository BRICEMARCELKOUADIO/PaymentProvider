using PaymentProvider.Domains.PaiementAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProvider.Domains.PaiementAggregate.Abstrations
{
    public interface ipaiement
    {
        Task<PaiementResult<string>> GetSignature(decimal amount, int transactionId);
        PaiementResult<string> GetPaiementUrl(decimal amount, int transactionId, string signature, string userId);
        Task<PaiementResult<StatutPayment>> GetPaiementInfos(int transactionId);
    }
}
