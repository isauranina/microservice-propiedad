using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface ITipoPropiedadRepository: IGenericRepository<TipoPropiedad>
    {
        public Task<IEnumerable<TipoPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
