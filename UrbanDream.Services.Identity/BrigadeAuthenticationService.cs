using BazarJok.Contracts.Options;
using BazarJok.DataAccess.Providers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BazarJok.DataAccess.Models.Users;
using UrbanDream.Contracts.Dtos;

namespace BazarJok.Services.Identity
{
    public class BrigadeAuthenticationService
    {
        private readonly BrigadeProvider _brigadeProvider;

        private SecretOption SecretOptions { get; }

        public BrigadeAuthenticationService(BrigadeProvider brigadeProvider, IOptions<SecretOption> secretOptions)
        {
            SecretOptions = secretOptions.Value;
            this._brigadeProvider = brigadeProvider;
        }


        /// <summary>
        ///     Get Jwt token by exited user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <exception cref="ArgumentException">User is not found</exception>
        /// <returns>Jwt token</returns>
        public async Task<string> Authenticate(string login, string password)
        {
            // Find data by arguments
            var brigade = await _brigadeProvider.GetByLogin(login);

            // if user is not found, throw exception
            if (!BCrypt.Net.BCrypt.Verify(password, brigade.PasswordHash))
                throw new ArgumentException("Incorrect password");

            return GenerateJwtToken(brigade.Login, brigade.Role);
        }

        public async Task AddBrigade(string login, string password, string brigadeName, string brigadeAddress, int count)
        {
            var support = new Brigade()
            {
                Login = login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                BrigadeName = brigadeName,
                Role = BrigadeRole.Brigade,
                BrigadeWorkAddress = brigadeAddress,
                BrigadeWorkersCount = count
            };

            await _brigadeProvider.Add(support);
        }


        private string GenerateJwtToken(string login, BrigadeRole role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretOptions.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.UserData, login),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }


        /// <summary>
        ///    Token decryption
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="ArgumentException">throws when could not parse claims</exception>
        /// <returns>Owner's data</returns>
        private AdminClaimsDto DecryptToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            if (tokenS?.Claims is List<Claim> claims)
            {
                return new AdminClaimsDto
                {
                    Login = claims[0].Value,
                    Role = (AdminRole)((Enum.TryParse(typeof(AdminRole), claims[1].Value, true, out var role)
                        ? role : throw new ArgumentException()) ?? throw new ArgumentException())
                };
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Gets Admin by headers from Request
        /// Usage in controllers: 
        /// GetAdminByHeaders(Request.Headers[HeaderNames.Authorization].ToArray())
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<Brigade> GetBrigadeByHeaders(string[] headers)
        {
            var token = headers[0].Replace("Bearer ", "");
           
            var result = await _brigadeProvider.GetByLogin(DecryptToken(token).Login);
            
            return result;
        }

    }
}
