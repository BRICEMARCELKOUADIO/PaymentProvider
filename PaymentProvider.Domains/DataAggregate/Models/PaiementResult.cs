using PaymentProvider.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Domains.DataAggregate.Models
{
    public class PaiementResult<T>
    {

        public PaiementResult()
        {
        }

        public PaiementResult(T response)
        {
            Response = response;
        }

        public StatusCode ResultCode { get; set; }

        
        public string Message { get; set; }
        
        public T Response { get; set; }
    }
}
