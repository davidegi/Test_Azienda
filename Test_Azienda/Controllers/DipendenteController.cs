using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test_Azienda1.Application.Mediatr.Queries;

namespace Test_Azienda1.Controllers
{
    // Controller per le operazioni sui dipendenti
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DipendenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DipendenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Servizio per estrazione di un dipendente tramite identificativo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetDipendenteById")]
        public async Task<IActionResult> GetDipendenteById([FromQuery]int id)
        {
            try
            {
                GetDipendenteQuery query = new(id);
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound($"Dipendente con ID {id} non trovato");
                return Ok(result);
            }
            catch (Exception)
            {
                var message = $"Errore durante il recupero del dipendente con ID {id}";
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
        }
    }
}
