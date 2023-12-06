using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Domain.Models.Administracion;
using Infrastructure.Persistence;
using Dapper;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administracion
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AdministracionContext _administracionContext;
        public LoginRepository(AdministracionContext administracionContext)
        {
            _administracionContext = administracionContext;
        }

        public async Task<Usuario> BuscarUsuario(UsuarioDto usuarioDto)
        {


            Usuario usuario=null;
            if (usuarioDto.cuenta == "admin" && usuarioDto.contrasena == "Clave*")
            {
                usuario = new Usuario();
                usuario.cuenta = "admin";
                usuario.nombre = "ADMIN";
                usuario.rol = "Administrador";
                usuario.estado = "AC";
            }
            return usuario;
        }


    }
}
