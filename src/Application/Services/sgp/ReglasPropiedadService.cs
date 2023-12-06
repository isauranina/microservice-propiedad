using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class ReglasPropiedadService : GenericService<ReglasPropiedad>,  IReglasPropiedadService
    {
        private readonly IReglasPropiedadRepository _reglasPropiedadRepository;

        public ReglasPropiedadService(IReglasPropiedadRepository reglasPropiedadRepository): base(reglasPropiedadRepository)
        {
            _reglasPropiedadRepository = reglasPropiedadRepository;
        }

        public async Task<RespuestaListado<ReglasPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<ReglasPropiedadDto>(){
                response = await _reglasPropiedadRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

