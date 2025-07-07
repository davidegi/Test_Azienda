using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Helpers;

namespace Test_Azienda1.Application.Mediatr.Commands
{
    public record AziendaDeleteCommand(AziendaDto aziendaDelete) : IRequest<string>;
    public class AziendaDeleteCommandHandler : IRequestHandler<AziendaDeleteCommand, string>
    {
        private readonly MyDbContext context;
        private readonly UserInformation userInformation;
        public AziendaDeleteCommandHandler(MyDbContext context, UserInformation userInformation)
        {
            this.context = context;
            this.userInformation = userInformation;
        }
        public async Task<string> Handle(AziendaDeleteCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = $"La cancellazione dell'azienda con ID: {request.aziendaDelete.IDAzienda}" +
                $" è stata effettuata con successo!";
            try
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    Azienda aziendaDelete = await context.Azienda
                    .Where(x => x.IDAzienda == request.aziendaDelete.IDAzienda)
                    .FirstOrDefaultAsync();

                    aziendaDelete.DataCancellazione = DateTime.Now;
                    context.Update(aziendaDelete);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                returnMessage = $"Errore durante la cancellazione dell'azienda";
            }
            return returnMessage;
        }
    }
}
