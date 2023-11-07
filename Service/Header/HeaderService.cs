using cotr.backend.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cotr.backend.Service.Header
{
    public class HeaderService : IHeaderService
    {
        public int GetTokenSubUserId(IHeaderDictionary headers)
        {
            if(!headers.TryGetValue("Authorization", out var value)) throw new ApiException(400, "No se ha encontrado información de la autorización en el header");

            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken token = handler.ReadJwtToken(value.ToString().Replace("Bearer ", ""));
            Claim subClaim = token.Claims.FirstOrDefault(claim => claim.Type == "sub") ?? throw new ApiException(400, "El token no especifica el usuario");

            if (!int.TryParse(subClaim.Value, out int userId)) throw new ApiException(400, "El claim sub del token no corresponde con el formato de usuario de la aplicación");
            return userId;

        }
    }
}
