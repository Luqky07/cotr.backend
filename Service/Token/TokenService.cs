using cotr.backend.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cotr.backend.Service.Token
{
    public class TokenService: ITokenService
    {
        private IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetToken(bool isAccess)
        {
            Jwt tokenConfig = configuration.GetSection("JwtConfiguration:TokenApi").Get<Jwt>() ?? throw new ApiException(500, "Error al cargar la configuración del token JWT");

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sid, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("token_type", isAccess ? "Access" : "Refresh")
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(isAccess ? tokenConfig.AccessKey : tokenConfig.RefreshKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                tokenConfig.Issuer,
                tokenConfig.Audience,
                claims,
                expires: isAccess ? DateTime.Now.AddMinutes(tokenConfig.DurationInMinutes) : DateTime.Now.AddMinutes((DateTime.Now - DateTime.Today).TotalMinutes),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}