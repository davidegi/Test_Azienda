using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Commands
{
    public record AziendaUpdateCommand(AziendaDto AziendaUpdate) : IRequest<string>;

    public class AziendaUpdateCommandHandler : IRequestHandler<AziendaUpdateCommand, string>
    {
        private readonly MyDbContext _dbContext;
        private readonly UserInformation _userInformation;
        private readonly IMapper _mapper;

        public AziendaUpdateCommandHandler(MyDbContext dbContext, UserInformation userInformation, IMapper mapper)
        {
            _dbContext = dbContext;
            _userInformation = userInformation;
            _mapper = mapper;
        }

        public async Task<string> Handle(AziendaUpdateCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = $"Update della azienda {request.AziendaUpdate.IDAzienda} eseguito con successo!";

            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                Azienda aziendaUpdate = await _dbContext.Azienda
                    .Where(x => x.IDAzienda == request.AziendaUpdate.IDAzienda)
                    .FirstOrDefaultAsync(cancellationToken);

                if (aziendaUpdate == null)
                {
                    returnMessage = $"L'azienda con ID: {request.AziendaUpdate.IDAzienda} non è stata trovata";
                }

                aziendaUpdate = _mapper.Map(request.AziendaUpdate, aziendaUpdate); // <--- aggiorna la stessa istanza tracciata

                //aziendaUpdate.CodiceFiscale = request.aziendaUpdate?.CodiceFiscale.Trim();
                //aziendaUpdate.PartitaIva = request.aziendaUpdate?.PartitaIva.Trim();
                //aziendaUpdate.Descrizione = request.aziendaUpdate?.Descrizione.Trim();
                //aziendaUpdate.Capitale = request.aziendaUpdate?.Capitale;

                _dbContext.Update(aziendaUpdate);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception)
            {
                returnMessage = "Errore durante la modifica dell'azienda";
            }
            return returnMessage;
        }
    }
    
}
