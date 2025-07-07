using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Azienda1.Application.DTO;
using Test_Azienda1.Domain.Table;
using Test_Azienda1.Utilities.Services;
using Mapper = Test_Azienda1.Application.Mapper.Profiles.Mapper;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly MyDbContext _context;

    public AuthController(IMediator mediator, MyDbContext myDbContext)
    {
        _mediator = mediator;
        _context = myDbContext;
    }

    [HttpGet("login")]
    public async Task<LoginResponseDto> Login([FromQuery] LoginRequestDto loginDto)
    {
        LoginResponseDto loginResponse = new LoginResponseDto();

        try
        {
            var utente = await _context.Utente.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (utente != null)

                if (utente.Password == loginDto.Password)
                {
                    // mapper
                    UtenteDto utenteDto = Mapper.Map<UtenteDto>(utente);

                    // generateToken
                    string token = TokenService.GenerateSimpleToken(utenteDto);

                    // return LoginResponseDto
                    loginResponse.Message = "Corretto";
                    loginResponse.Success = true;
                    loginResponse.Token = token;

                    loginResponse = new LoginResponseDto
                    {
                        Token = token,
                        Success = true,
                        Message = "login effettuata con successo"
                    };
                }
                else
                    loginResponse = new LoginResponseDto
                    {
                        Token = null,
                        Success = false,
                        Message = "Password o username inseriti non corretti"
                    };
            else
                loginResponse = new LoginResponseDto
                {
                    Token = null,
                    Success = false,
                    Message = "Utente non trovato"
                };

        }
        catch (Exception)
        {
            loginResponse = new LoginResponseDto
            {
                Token = null,
                Success = false,
                Message = "Errore interno durante il login."
            };
        }
        return loginResponse;
    }
}
