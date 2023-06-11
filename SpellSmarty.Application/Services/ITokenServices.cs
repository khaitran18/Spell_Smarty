using System.Security.Claims;
namespace SpellSmarty.Application.Services
{
    public interface ITokenServices
    {
        public string GenerateJWTToken((int userId, string userName, string roles) userDetails);

        /// <summary>
        /// using to extract claim from token from header.Authorization
        /// example use: ValidateToken(tokenString)?.FindFirst("ClaimName")?.Value
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateToken(string jwtToken);
        
    }
}
