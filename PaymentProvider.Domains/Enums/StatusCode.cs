using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Domains.Enums
{
    public enum StatusCode
    {
        Success,
        Failed,
        SignatureFailed = 609,
    }
}
