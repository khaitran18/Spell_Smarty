﻿using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SpellSmarty.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SpellSmarty.Infrastructure.Services
{
    public class TokenService : ITokenServices
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiryMinutes;

        public TokenService(string key, string issuer, string audience, string expiryMinutes)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateJWTToken((int? userId, string? userName, string? roles) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var (userId, userName, roles) = userDetails;

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()));
            claims.Add(new Claim("username", userName));
            claims.Add(new Claim(ClaimTypes.Role, roles));


            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiryMinutes)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            try
            {
                jwtToken = jwtToken.Trim();
                // token now include bearer, we dont need bearer
                var index = jwtToken.IndexOf(" ");
                if (index != -1)
                {
                    jwtToken = jwtToken.Substring(index + 1);
                }

                IdentityModelEventSource.ShowPII = true;
                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;

                validationParameters.ValidAudience = _audience.ToLower();
                validationParameters.ValidIssuer = _issuer.ToLower();
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
                return principal;
            }
            catch (Exception e)
            {
                throw new Exception("Error in validating token");
            }
        }
    }
}
