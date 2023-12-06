using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IEstadoPropiedadRepository: IGenericRepository<EstadoPropiedad>
    {
        public Task<IEnumerable<EstadoPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
