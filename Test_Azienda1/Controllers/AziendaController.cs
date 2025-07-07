using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Application.Mediatr.Commands;
using Test_Azienda1.Application.Mediatr.Queries;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Test_Azienda1.Controllers
{
    /// Controller per le operazioni in Azienda
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AziendaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AziendaController(IMediator? mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Servizio per estrazione di un'azienda tramite identificativo
        /// </summary>
        /// <param name="Id></param>
        /// <returns></returns>
        [HttpGet(Name = "GetAziendaById")]
        public async Task<IActionResult> GetAziendaById([FromQuery] int id)
        {
            try
            {
                GetAziendaQuery query = new GetAziendaQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound($"Azienda con ID {id} non trovata");
                return Ok(result);
            }
            catch (Exception)
            {
                var message = $"Errore durante il recupero dell'azienda con Id {id}";
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "AziendaInsert")]
        public async Task<IActionResult> AziendaInsert([FromBody] AziendaDto aziendaInsert)
        {
            try
            {
                AziendaInsertCommand query = new AziendaInsertCommand(aziendaInsert);
                var result = await _mediator.Send(query);
                var errorMessages = ("operazione completata con risultato: {result}", result);
                return Ok(result);
            }
            catch (Exception e)
            {
                var errorMessages = (e, "Errore generico durante l'inserimento dell'azienda");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "AziendaDelete")]
        public async Task<IActionResult> AziendaDelete([FromBody] AziendaDto aziendaDelete)
        {
            try
            {
                AziendaDeleteCommand deleteQuery = new AziendaDeleteCommand(aziendaDelete);
                var deleteResult = await _mediator.Send(deleteQuery);
                return Ok(deleteResult);
            }
            catch (Exception e)
            {
                var errorMessages = (e, "Errore generico durante la cancellazione dell'azienda");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
