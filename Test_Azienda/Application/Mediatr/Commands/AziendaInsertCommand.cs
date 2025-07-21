using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Commands
{
    public record AziendaInsertCommand(AziendaDto AziendaInsert) : IRequest<string>;

    public class AziendaInsertCommandHandler : IRequestHandler<AziendaInsertCommand, string>
    {
        private readonly MyDbContext _context;
        private readonly UserInformation _userInformation;
        private readonly IMapper _mapper;

        public AziendaInsertCommandHandler(MyDbContext context, UserInformation userInformation, IMapper mapper)
        {
            _context = context;
            _userInformation = userInformation;
            _mapper = mapper;
        }
        public async Task<string> Handle(AziendaInsertCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = "inserimento di Azienda eseguito con successo";

            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                Azienda aziendaInsert = await _context.Azienda
                    .Where(x => x.IDAzienda == request.AziendaInsert.IDAzienda)
                    .FirstOrDefaultAsync(cancellationToken);

                aziendaInsert = _mapper.Map(request.AziendaInsert, aziendaInsert);
                /*Azienda azienda = new()
                {
                    CodiceFiscale = request.AziendaInsert.CodiceFiscale,
                    PartitaIva = request.AziendaInsert.PartitaIva,
                    Descrizione = request.AziendaInsert.Descrizione,
                    Capitale = request.AziendaInsert.Capitale,
                    IDRuolo = request.AziendaInsert.IDRuolo,
                    DataInizioAtt = request.AziendaInsert.DataInizioAtt,
                    DataFineAtt = request.AziendaInsert.DataFineAtt
                };*/
                _context.Add(aziendaInsert);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                returnMessage = "Errore durante l'inserimento";
            }
            return returnMessage;
        }
    } 
}
