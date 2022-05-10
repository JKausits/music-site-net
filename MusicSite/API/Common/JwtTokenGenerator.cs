using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicSite.API.Features;
using MusicSite.API.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicSite.API.Common
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserTokenDto user);
    }

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(UserTokenDto user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        #region Private
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(UserTokenDto user)
        {
            return new List<Claim>
            {
                new Claim(nameof(user.Id).ToLower(), user.Id.ToString()),
                new Claim(nameof(user.Email).ToLower(), user.Email)
            };
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
        #endregion
    }
}
