using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProvider.Domains.PaiementAggregate.Abstrations;
using PaymentProvider.Domains.PaiementAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProvider.Controllers
{
    [Route("api/paiement")]
    [ApiController]
    public class PaiementController : ControllerBase
    {

        private readonly IPaiement _paiement;
        public PaiementController(IPaiement paiement)
        {
            _paiement = paiement ?? throw new ArgumentNullException(nameof(paiement));
        }

        [Route("GetSignature")]
        [HttpGet]
        public async Task<IActionResult> GetSignatureAsync(decimal amount)
        {
            try
            {
                Paiement paiement = new Paiement() { Amount = amount };

                var response  = await _paiement.GetSignature(paiement);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
