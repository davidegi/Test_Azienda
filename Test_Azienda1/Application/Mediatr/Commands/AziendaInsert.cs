using MediatR;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Helpers;

namespace Test_Azienda1.Application.Mediatr.Commands
{
    public record AziendaInsertCommand(AziendaDto AziendaInsert) : IRequest<string>;

    public class AziendaInsertCommandHandler : IRequestHandler<AziendaInsertCommand, string>
    {
        private readonly MyDbContext context;
        private readonly UserInformation userInformation;

        public AziendaInsertCommandHandler(MyDbContext context, UserInformation userInformation)
        {
            this.context = context;
            this.userInformation = userInformation;
        }

        public async Task<string> Handle(AziendaInsertCommand request, CancellationToken cancellationToken)
        {
            
            string returnMessage = "inserimento di Azienda eseguito con successo";

            try
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    Azienda azienda = new Azienda
                    {
                        CodiceFiscale = request.AziendaInsert.CodiceFiscale,
                        PartitaIva = request.AziendaInsert.PartitaIva,
                        Descrizione = request.AziendaInsert.Descrizione,
                        Capitale = request.AziendaInsert.Capitale,
                        IDRuolo = request.AziendaInsert.IDRuolo,
                        DataInizioAtt = request.AziendaInsert.DataInizioAtt,
                        DataFineAtt = request.AziendaInsert.DataFineAtt
                    };
                    context.Add(azienda);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                returnMessage = $"Errore durante l'inserimento";
            }

            return returnMessage;
        }
    }
    
}
