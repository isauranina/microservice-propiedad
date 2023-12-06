using Application.DTOs.Administracion;
using Domain.Models.Administracion;


namespace Application.Interfaces.IRepositories.Administracion
{
    public interface ILoginRepository
    {
        public Task<Usuario> BuscarUsuario(UsuarioDto usuarioDto);
    }
}
