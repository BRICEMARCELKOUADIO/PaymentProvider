using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Domains.Enums
{
    public enum StatusCode
    {
        Success,
        Failed,
        SUCCES = 00,
        PAYMENT_FAILED = 600,
        MERCHANT_NOT_FOUND = 601,
    }
}
