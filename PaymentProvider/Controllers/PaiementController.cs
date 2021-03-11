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

        [HttpGet("GetSignature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSignatureAsync(decimal amount, int transactionId)
        {
            try
            {
                var response  = await _paiement.GetSignature(amount, transactionId);

                if (response.ResultCode != 0)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetPaimentUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaimentUrlAsync(decimal amount, int transactionId, string signature)
        {
            try
            {
                var response = await _paiement.GetPaiementUrl(amount, transactionId, signature);

                if (response.ResultCode != 0)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetPaimentInfos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaimentInfosAsync(int transactionId)
        {
            try
            {
                var response = await _paiement.GetPaiementInfos(transactionId);

                if (response.ResultCode != 0)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
