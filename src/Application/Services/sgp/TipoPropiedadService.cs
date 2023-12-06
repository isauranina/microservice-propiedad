using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class TipoPropiedadService : GenericService<TipoPropiedad>,  ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;

        public TipoPropiedadService(ITipoPropiedadRepository tipoPropiedadRepository): base(tipoPropiedadRepository)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
        }

        public async Task<RespuestaListado<TipoPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<TipoPropiedadDto>(){
                response = await _tipoPropiedadRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

    }

}

