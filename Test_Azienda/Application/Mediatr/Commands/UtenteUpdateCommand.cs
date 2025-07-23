using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;
using Test_Azienda.Utilities.Helpers;

namespace Test_Azienda.Application.Mediatr.Commands
{
    public record UtenteUpdateCommand(UtenteUpdateDto UtenteUpdate) : IRequest<string>;

    public class UtenteUpdateCommandHandler : IRequestHandler<UtenteUpdateCommand, string>
    {
        private readonly MyDbContext _context;
        private readonly UserInformation _userInformation;
        private readonly IMapper _mapper;

        public UtenteUpdateCommandHandler(MyDbContext myDbContext, UserInformation userInformation, IMapper mapper)
        {
            _context = myDbContext;
            _userInformation = userInformation;
            _mapper = mapper;
        }

        public async Task<string> Handle(UtenteUpdateCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = $"Aggiornamento dell'utente {request.UtenteUpdate.IDUtente} avvenuto con successo!";

            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                Utente utenteUpdate = await _context.Utente
                    .Where(x => x.IDUtente == request.UtenteUpdate.IDUtente)
                    .FirstOrDefaultAsync(cancellationToken);

                if (utenteUpdate is null)
                {
                    returnMessage = $"Utente con ID: {request.UtenteUpdate.IDUtente} non trovato!";
                }

                utenteUpdate.Username = utenteUpdate.Username != request.UtenteUpdate.Username
                    ? request.UtenteUpdate.Username
                    : utenteUpdate.Username;

                utenteUpdate.Email = utenteUpdate.Email != request.UtenteUpdate.Email
                    ? request.UtenteUpdate.Email
                    : utenteUpdate.Email;

                utenteUpdate.Password = utenteUpdate.Password != request.UtenteUpdate.Password
                    ? request.UtenteUpdate.Password
                    : utenteUpdate.Password;

                utenteUpdate = _mapper.Map(request.UtenteUpdate, utenteUpdate);
                _context.Update(utenteUpdate);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                returnMessage = "Errore durante la modifica dell'utente";
            }
            return returnMessage;
        }
    }
}
