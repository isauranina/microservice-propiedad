using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IPropiedadEstadoRepository: IGenericRepository<PropiedadEstado>
    {
        public Task<IEnumerable<PropiedadEstadoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
