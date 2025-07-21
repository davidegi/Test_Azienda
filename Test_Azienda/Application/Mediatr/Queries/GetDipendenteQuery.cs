using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.Mapper.Profiles;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Queries
{
    public record GetDipendenteQuery : IRequest<DipendenteDto>
    {
        public GetDipendenteQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetDipendenteQHandler : IRequestHandler<GetDipendenteQuery, DipendenteDto>
    {
        private readonly UserInformation _userInformation;
        private readonly MyDbContext _myDbContext;


        public GetDipendenteQHandler(UserInformation userInformation, MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            this._userInformation = userInformation;
        }

        public async Task<DipendenteDto> Handle(GetDipendenteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var dipendente = await _myDbContext.Dipendente
                    .Where(x => x.IDDipendente == request.Id)
                    .FirstOrDefaultAsync();

                DipendenteDto dipendenteDto = Mapper.Map<DipendenteDto>(dipendente);

                if (dipendenteDto == null)
                {
                    var errorMessage = ($"Dipendente con ID {request.Id} non trovata", request.Id);
                    return null;
                }
                return dipendenteDto;
            }
            catch (Exception)
            {
                var errorMessage = ($"Errore generico durante il recupero del dipendente con ID {request.Id}", request.Id);
                throw;
            }
        }
    }
}
