using Entregas.API.Data;
using Entregas.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Entregas.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly EntregasContext context;

        public TokenRepository(IConfiguration configuration, EntregasContext ticketsDbContext)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Buscamos el usuario
            //string sucursalId = context.Usuarios.Where(x => x.LoginId == user.Id).FirstOrDefault().SucursalId.ToString();


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
                //new Claim("SucursalId", sucursalId),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
