using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IPropiedadServicioRepository: IGenericRepository<PropiedadServicio>
    {
        public Task<IEnumerable<PropiedadServicioDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
