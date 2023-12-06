using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.ISecurity;
using Application.Interfaces.IServices.Administracion;
using Domain.Models.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Administracion.Login
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private IPasswordHasher _passwordHasher;
        
        public LoginService(ILoginRepository loginRepository, IPasswordHasher passwordHasher)
        {
            _loginRepository = loginRepository;
            _passwordHasher = passwordHasher;
          
        }
        public async Task<Usuario?> ValidateUser(UsuarioDto usuarioDto)
        {
            var usuario = await _loginRepository.BuscarUsuario(usuarioDto);
            if (usuario != null)
            {
                
                    return usuario;
               
            }

            return null;
        }
        public async Task<Usuario> Populate(string cuenta)
        {
            var usuarioDto = new UsuarioDto { cuenta = cuenta, contrasena = "" };
            return await _loginRepository.BuscarUsuario(usuarioDto);
        }

       


    }
}
