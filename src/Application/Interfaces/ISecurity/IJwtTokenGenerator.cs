using Domain.Models.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ISecurity
{
    public interface IJwtTokenGenerator
    {
        Task<string> CreateToken(Usuario usuario);
        public string getTokenValue(ClaimsIdentity identity, string claim);
    }
}
