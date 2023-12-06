using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IData;
using Domain.Models.sgp;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Domain.Enums;
using Domain.Models.Data;

namespace Infrastructure.Repositories.sgp
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public PropiedadRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<PropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<PropiedadDto>? arrayDatos = new PropiedadDto[] { };
                string nombreFuncion = "sp_listado_propiedad";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<PropiedadDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RespuestaDB> EstadoPropiedad(VerificarPropiedad datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = $"sp_cambiar_estado_propiedad";

                respuesta = await _applicationDbContext.EjecutarProcedimiento(nombreFuncion, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ListaDto> traer_por_id(long codigo)
        {
            try
            {
                string nombreFuncion = $"sp_traer_propiedad";

                Hashtable parametros = new Hashtable();
                parametros.Add("_num_sec", codigo);

                var datos = await _applicationDbContext.TraerObjeto<ListaDto>(nombreFuncion, parametros);

                return datos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

