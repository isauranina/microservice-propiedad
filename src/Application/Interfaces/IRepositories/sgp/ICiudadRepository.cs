using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface ICiudadRepository: IGenericRepository<Ciudad>
    {
        public Task<IEnumerable<CiudadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
