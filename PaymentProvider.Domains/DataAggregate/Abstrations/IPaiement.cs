using PaymentProvider.Domains.DataAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProvider.Domains.DataAggregate.Abstrations
{
    public interface IPaiement
    {
        Task<PaiementResult<string>> GetSignature(string transactionId);
        Task<PaiementResult<string>> GetPaiementUrl(string PhoneNumber);
    }
}
