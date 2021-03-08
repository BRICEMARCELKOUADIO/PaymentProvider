using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProvider.Controllers
{
    [Route("api/paiement")]
    [ApiController]
    public class PaiementController : Controller
    {
        public PaiementController()
        {

        }

        [Route("GetSignature")]
        [HttpGet]
        public async Task<IActionResult> GetSignatureAsync()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
