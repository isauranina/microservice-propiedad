using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Interfaces.IServices.sgp
{
    public interface IPropiedadServicioService : IGenericService<PropiedadServicio>
    {
        public Task<RespuestaListado<PropiedadServicioDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
      
    }
}
