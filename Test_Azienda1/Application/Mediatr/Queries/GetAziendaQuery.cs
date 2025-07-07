using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Helpers;

namespace Test_Azienda1.Application.Mediatr.Queries
{
    public record GetAziendaQuery : IRequest<AziendaDto>
    {
        public GetAziendaQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }  
    }

    public class GetAziendaQueryHandler : IRequestHandler<GetAziendaQuery, AziendaDto>
    {
        private readonly UserInformation userInformation;
        private readonly MyDbContext _myDbContext;

        public GetAziendaQueryHandler(UserInformation userInformation, MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            this.userInformation = userInformation; 
        }

        public async Task<AziendaDto> Handle(GetAziendaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var azienda = await _myDbContext.Azienda
                    .Where(x => x.IDAzienda == request.Id)
                    .FirstOrDefaultAsync();

                AziendaDto aziendaDto = Mapper.Profiles.Mapper.Map<AziendaDto>(azienda);

                if (aziendaDto == null)
                {
                    var messaggio1 = ($"Azienda con Id {request.Id} non trovata", request.Id);
                    return null;
                }
                return aziendaDto;
            }
            catch (Exception ex)
            {
                var messaggio2 = (ex, $"Errore durante il recupero dell'azienda con Id {request.Id}", request.Id);
                throw;
            } 
        }
    }
}