using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IData;
using Domain.Models.sgp;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.sgp
{
    public class EstadoPropiedadRepository : GenericRepository<EstadoPropiedad>, IEstadoPropiedadRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public EstadoPropiedadRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<EstadoPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<EstadoPropiedadDto>? arrayDatos = new EstadoPropiedadDto[] { };
                string nombreFuncion = "sp_listado_estado_propiedad";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<EstadoPropiedadDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

