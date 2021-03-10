using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProvider.Infrastructure.Dto
{
    public class StatutSignatureDto
    {
        public StatutSignatureDetailDto status { get; set; }
    }

    public class StatutSignatureDetailDto
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
