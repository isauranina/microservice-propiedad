using Application.DTOs.Administracion;
using Domain.Models.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Administracion
{
    public interface ILoginService
    {
        public Task<Usuario?> ValidateUser(UsuarioDto usuarioDto);

        public Task<Usuario> Populate(string cuenta);

    }
}
