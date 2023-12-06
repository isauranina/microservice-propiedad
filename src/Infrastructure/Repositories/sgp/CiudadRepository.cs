using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IData;
using Domain.Models.sgp;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.sgp
{
    public class CiudadRepository : GenericRepository<Ciudad>, ICiudadRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public CiudadRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<CiudadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<CiudadDto>? arrayDatos = new CiudadDto[] { };
                string nombreFuncion = "sp_listado_ciudad";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<CiudadDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

