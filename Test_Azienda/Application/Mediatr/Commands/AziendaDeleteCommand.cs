using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Helpers;

namespace Test_Azienda1.Application.Mediatr.Commands
{
    public record AziendaDeleteCommand(AziendaDto AziendaDelete) : IRequest<string>;
    public class AziendaDeleteCommandHandler : IRequestHandler<AziendaDeleteCommand, string>
    {
        private readonly MyDbContext _context;
        private readonly UserInformation _userInformation;
        public AziendaDeleteCommandHandler(MyDbContext context, UserInformation userInformation)
        {
            _context = context;
            _userInformation = userInformation;
        }
        public async Task<string> Handle(AziendaDeleteCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = $"La cancellazione dell'azienda con ID: {request.AziendaDelete.IDAzienda}" +
                " è stata effettuata con successo!";
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                Azienda aziendaDelete = await _context.Azienda
                .Where(x => x.IDAzienda == request.AziendaDelete.IDAzienda)
                .FirstOrDefaultAsync(cancellationToken);

                aziendaDelete.DataCancellazione = DateTime.Now;
                _context.Update(aziendaDelete);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                returnMessage = "Errore durante la cancellazione dell'azienda";
            }
            return returnMessage;
        }
    }
}
