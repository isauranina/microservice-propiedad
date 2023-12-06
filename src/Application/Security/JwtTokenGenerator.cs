using Application.Interfaces.ISecurity;
using Application.Utils;
using Domain.Models.Administracion;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<string> CreateToken(Usuario usuario)
        {
            string? SecretKey = _jwtOptions.SecretKey ?? "";
            string? Issuer = _jwtOptions.Issuer;
            string? Audience = _jwtOptions.Audience;

            string? cuenta = usuario.cuenta ?? "";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, cuenta ),
                    new Claim(ClaimTypes.NameIdentifier ,cuenta),
                    new Claim(ClaimTypes.Sid, usuario.num_sec.ToString() ),
                    new Claim(ClaimCustom.NsecRol, usuario.nsec_rol.ToString() ),
                    new Claim(ClaimTypes.Role, usuario.rol.ToString() ),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)
             };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string getTokenValue(ClaimsIdentity identity, string claim)
        {
            if (identity != null)
            {
                var value = identity.FindFirst(claim!)!.Value;
                return value;
            }
            return "";
        }


    }
}
