using PaymentProvider.Domains.PaiementAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProvider.Domains.PaiementAggregate.Abstrations
{
    public interface IPaiement
    {
        Task<PaiementResult<string>> GetSignature(decimal amount, int transactionId);
        Task<PaiementResult<string>> GetPaiementUrl(decimal amount, int transactionId, string signature);
        Task<PaiementResult<string>> GetPaiementInfos(int transactionId);
    }
}
