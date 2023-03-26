using HairdresserAppointmentAPI.Handler.Abstract;
using HairdresserAppointmentAPI.Handler.Model;
using HairdresserAppointmentAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HairdresserAppointmentAPI.Handler.Concrete
{
    public class TokenHandler : ITokenHandler
    {
        public Token CreateAccessToken(DateTime expireDate, IList<Claim> claims)
        {
            Token token = new Token();
            var configuration = HelperMethods.GetConfiguration();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.expireDate = expireDate;

            JwtSecurityToken jwtSecurityToken = new(
                audience: configuration["JWT:Audience"],
                issuer: configuration["JWT:Issuer"],
                claims: claims,
                expires: token.expireDate,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler securityTokenHandler = new();

            token.token = securityTokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
