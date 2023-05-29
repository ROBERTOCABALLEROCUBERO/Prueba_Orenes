using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Orenes.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Orenes.Services.Implementaciones
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _config;

        public SecurityService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public string Encriptar(string clave)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(null, clave);
        }

        public bool Desencriptar(string claveEncriptada, string clave)
        {
            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, claveEncriptada, clave);
            return result == PasswordVerificationResult.Success;
        }

    }
   
}
     
    

