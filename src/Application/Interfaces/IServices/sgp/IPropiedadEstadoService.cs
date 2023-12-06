using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Interfaces.IServices.sgp
{
    public interface IPropiedadEstadoService : IGenericService<PropiedadEstado>
    {
        public Task<RespuestaListado<PropiedadEstadoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
