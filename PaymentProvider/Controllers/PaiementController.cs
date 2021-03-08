using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProvider.Domains.PaiementAggregate.Abstrations;
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

        private readonly IPaiement _paiement;
        public PaiementController(IPaiement paiement)
        {
            _paiement = paiement ?? throw new ArgumentNullException(nameof(paiement));
        }

        [Route("GetSignature")]
        [HttpGet]
        public async Task<IActionResult> GetSignatureAsync()
        {
            try
            {
                var id = "0";

                var response  = await _paiement.GetSignature(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
