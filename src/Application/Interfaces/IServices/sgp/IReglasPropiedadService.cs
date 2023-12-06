using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Interfaces.IServices.sgp
{
    public interface IReglasPropiedadService : IGenericService<ReglasPropiedad>
    {
        public Task<RespuestaListado<ReglasPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
