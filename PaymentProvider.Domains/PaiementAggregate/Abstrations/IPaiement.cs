using PaymentProvider.Domains.PaiementAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProvider.Domains.PaiementAggregate.Abstrations
{
    public interface IPaiement
    {
        Task<PaiementResult<string>> GetSignature(Paiement paiement);
        Task<PaiementResult<string>> GetPaiementUrl();
    }
}
