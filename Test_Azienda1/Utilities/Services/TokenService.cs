using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Test_Azienda1.Application.DTO;

namespace Test_Azienda1.Utilities.Services
{
        /// <summary>
        /// Classe che governa tutte le funzionalità relative alla gestione dei token JWT
        /// </summary>
        public class TokenService
        {
            /// <summary>
            /// Parametro di configurazione che imposta la validità in minuti del token
            /// </summary>
            private static int lifetime;
            /// <summary>
            /// Chiave di cifratura per la firma del token (viene impostata in fase di configurazione)
            /// </summary>
            public static SymmetricSecurityKey Key { get; private set; }
            /// <summary>
            /// Indica se Il gestore dei tokens è stato inizializzato
            /// </summary>
            public static bool IsInitialized { get; private set; } = false;
            /// <summary>
            /// Inizializzatore del geswtore dei token. Vengono impostati durata del token e chiave di cifratura
            /// </summary>
            /// <param name="tokenLifetimeMinutes">durata di validità in munuti del token</param>
            /// <param name="signingKey">chiave di cifratura (deve essere di almento 16 caratteri)</param>
            public static void Config(int tokenLifetimeMinutes, string signingKey)
            {
                if (tokenLifetimeMinutes <= 0)
                    throw new Exception("tokenLifetimeMinutes must be positive");
                if (signingKey.Length < 16)
                    throw new Exception("signingKey length must be 16 characters minimum");

                lifetime = tokenLifetimeMinutes;
                Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
                IsInitialized = true;
            }

            /// <summary>
            /// Genera un token semplice con 
            /// </summary>
            /// <param name="user">utente loggato all'applicazione</param>
            /// <returns></returns>
            public static string GenerateSimpleToken(UtenteDto utente)
            {
                if (!IsInitialized)
                    throw new Exception("TokenService not initialized - use TokenServie.Config method");

                List<Claim> claims = new List<Claim>();

                if (utente != null)
                    claims.Add(new Claim(ClaimTypes.Name, utente.Username));
                    claims.Add(new Claim("Utente", JsonConvert.SerializeObject(utente)));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = Assembly.GetEntryAssembly().GetName().Name,
                    Expires = DateTime.UtcNow.AddHours(lifetime),
                    SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                return token.RawData;
            }
            /// <summary>
            /// Validatore Della scadenza del token
            /// </summary>
            /// <param name="notBefore"></param>
            /// <param name="expires"></param>
            /// <param name="token"></param>
            /// <param name="params"></param>
            /// <returns></returns>
            public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
            {
                if (notBefore == null)
                    return false;
                if (expires == null)
                    return false;
                return notBefore < DateTime.UtcNow && expires > DateTime.UtcNow;
            }
        }
    }
