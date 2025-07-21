using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Commands
{
    public record UtenteDeleteCommand (UtenteDeleteDto UtenteDelete) : IRequest<string>;

    public class UtenteDeleteCommandHandler : IRequestHandler<UtenteDeleteCommand, string>
    {
        private readonly MyDbContext _dbContext;
        private readonly UserInformation _userInformation;

        public UtenteDeleteCommandHandler(MyDbContext dbContext, UserInformation userInformation)
        {
            _userInformation = userInformation;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(UtenteDeleteCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = $"La cancellazione dell'utente con ID: {request.UtenteDelete.IDUtente}" +
                " è stata effettuata con successo!";
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                Utente utenteDelete = await _dbContext.Utente
                    .Where(x => x.IDUtente == request.UtenteDelete.IDUtente)
                    .FirstOrDefaultAsync(cancellationToken);

                utenteDelete.DataCancellazione = DateTime.Now;
                _dbContext.Update(utenteDelete);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                returnMessage = "Errore durante la cancellazione dell'utente";
            }
            return returnMessage;
        }
    }
    
}
