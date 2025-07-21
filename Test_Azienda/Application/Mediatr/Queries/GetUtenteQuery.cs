using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Queries
{
    public record GetUtenteQuery : IRequest<UtenteDto>
    {
        public GetUtenteQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetUtenteQueryHandler : IRequestHandler<GetUtenteQuery, UtenteDto>
    {
        private readonly MyDbContext _context;
        private readonly UserInformation _userInformation;
        private readonly IMapper _mapper;

        public GetUtenteQueryHandler(MyDbContext context, UserInformation userInformation, IMapper mapper)
        {
            _context = context;
            _userInformation = userInformation;
            _mapper = mapper;
        }

        public async Task<UtenteDto> Handle (GetUtenteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var utente = await _context.Utente
                    .Where(x => x.IDUtente == request.Id)
                    .FirstOrDefaultAsync();
                UtenteDto utenteDto = _mapper.Map<UtenteDto>(utente);

                if (utenteDto == null)
                {
                    var errorMessage = ($"Utente con ID: {request.Id} non trovato", request.Id);
                    return null;
                }
                return utenteDto;
            }
            catch (Exception ex)
            {
                var errorMessage = (ex, $"Errore generico durante il recupero dell'utente con ID: {request.Id}",  request.Id);
                throw;
            }
        }
    }
}
