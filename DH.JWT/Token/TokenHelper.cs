using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DH.JWT.Token
{
    public enum TokenType
    {
        AccessToken=1,
        RefreshToken=2
    }
    public class TokenHelper : ITokenHelper
    {
        private IOptions<JWTConfig> _options;
        public TokenHelper(IOptions<JWTConfig> options)
        {
            _options = options;
        }
        public Token CreateAccessToken(SysUserer user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Code),
                new Claim(ClaimTypes.NameIdentifier,user.Name)

            };
            return CreateToken(claims,TokenType.AccessToken);
        }
        public ComplexToken CreateToken(SysUserer user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Code),
                new Claim(ClaimTypes.Name,user.Name)

            };
            return CreateToken(claims);
        }
        public ComplexToken CreateToken(Claim[] claims)
        {
            return new ComplexToken { AccessToken = CreateToken(claims,TokenType.AccessToken), RefreshToken = CreateToken(claims,TokenType.RefreshToken) };
        }
        private Token CreateToken(Claim[] claims,TokenType tokenType)
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(tokenType.Equals(TokenType.AccessToken)?_options.Value.AccessTokenExpiresMinutes:_options.Value.RefreshTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: tokenType.Equals(TokenType.AccessToken) ? _options.Value.Audience : _options.Value.RefreshTokenAudience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256));
            return new Token { TokenContent = new JwtSecurityTokenHandler().WriteToken(token), Expires = expires };
        }
        public Token RefreshToken(ClaimsPrincipal claimsPrincipal)
        {
            var code = claimsPrincipal.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier));
            if (null != code)
            {
                SysUserer user = new SysUserer
                {
                    Code = code.Value.ToString(),
                    Name = "renfushai",
                };
                return CreateAccessToken(user);
            }
            else
            {
                return null;
            }
        }
    }
}
