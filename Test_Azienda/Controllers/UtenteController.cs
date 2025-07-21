using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Test_Azienda.Application.DTO;
using Test_Azienda.Application.Mediatr.Commands;
using Test_Azienda.Application.Mediatr.Queries;

namespace Test_Azienda1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UtenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtenteDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetUtenteById")]
        public async Task<IActionResult> GetUtenteById([FromQuery] int id)
        {
            try
            {
                GetUtenteQuery query = new(id);
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound($"Utente con ID: {id} non trovato");
                return Ok(result);
            }
            catch (Exception)
            {
                var errorMessage = $"Errore dirante il recuper dell'utente con ID: {id}";
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }


        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtenteDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "InsertUtente")]
        public async Task<IActionResult> InsertUtente([FromBody] UtenteDto insertUtente)
        {
            try
            {
                UtenteInsertCommand query = new(insertUtente);
                var result = await _mediator.Send(query);
                var errorMessages = ($"operazione completata con risultato: {result}", result);
                return Ok(result);
            }
            catch (Exception)
            {
                var errorMessages = "Errore generico durante l'inserimento";
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessages);
            } 
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtenteDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "DeleteUtente")]

        public async Task<IActionResult> DeleteUtente([FromBody] UtenteDeleteDto deleteUtente)
        {
            try
            {
                UtenteDeleteCommand deleteQuery = new(deleteUtente);
                var deleteResult = await _mediator.Send(deleteQuery);
                return Ok(deleteResult);
            }
            catch (Exception)
            {
                var errorMessages = ("Errore generico durante la cancellazione dell'utente");
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessages);
            }
        }
    }
}
