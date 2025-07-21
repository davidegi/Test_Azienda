using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Helpers;

namespace Test_Azienda1.Application.Mediatr.Commands
{
    public record UtenteInsertCommand (UtenteDto UtenteInsert) : IRequest<string>;
    
    public class UtenteInsertCommandHandler : IRequestHandler<UtenteInsertCommand, string>
    {
        private readonly MyDbContext _context;
        private readonly UserInformation _userInformation;
        private readonly IMapper _mapper;

        public UtenteInsertCommandHandler(MyDbContext context, UserInformation userInformation, IMapper mapper)
        {
            _context = context;
            _userInformation = userInformation;
            _mapper = mapper;
        }

        public async Task<string> Handle(UtenteInsertCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = "Inserimento dell'utente avvenuto";

            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                Utente utenteInsert = await _context.Utente
                    .Where(u => u.IDUtente == request.UtenteInsert.IDUtente)
                    .FirstOrDefaultAsync(cancellationToken);

                utenteInsert = _mapper.Map(request.UtenteInsert, utenteInsert);

                /*Utente utente = new()
                {
                    Username = request.UtenteInsert.Username,
                    Email = request.UtenteInsert.Email,
                    Password = request.UtenteInsert.Password,
                    IDRuolo = request.UtenteInsert.IDRuolo
                };*/                                                   
                _context.Add(utenteInsert);
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
