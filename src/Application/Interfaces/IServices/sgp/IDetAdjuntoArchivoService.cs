using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Interfaces.IServices.sgp
{
    public interface IDetAdjuntoArchivoService : IGenericService<DetAdjuntoArchivo>
    {
        public Task<RespuestaListado<DetAdjuntoArchivoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
