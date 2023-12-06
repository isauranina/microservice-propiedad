using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IDetAdjuntoArchivoRepository: IGenericRepository<DetAdjuntoArchivo>
    {
        public Task<IEnumerable<DetAdjuntoArchivoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
