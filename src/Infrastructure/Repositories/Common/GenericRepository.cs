using Application.Interfaces.Common;
using Application.Interfaces.IData;
using Domain.Enums;
using Domain.Models.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IApplicationDbContext _conexion;
        private readonly string _tableName;

        public GenericRepository(IApplicationDbContext conexion)
        {
            _conexion = conexion;
            string className = typeof(T).Name;
            _tableName = Regex.Replace(className, @"\p{Lu}", "_$0").ToLower().Trim();


        }

        public async Task<T> BuscarPorNumSec(long numSec)
        {
            try
            {
                string nombreFuncion = $"sp_traer{_tableName}";

                Hashtable parametros = new Hashtable();
                parametros.Add("_num_sec", numSec);

                var datos = await this._conexion.TraerObjeto<T>(nombreFuncion, parametros);

                return datos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaDB> Eliminar(T datos)
        {
            try
            {
                RespuestaDB? respuesta = null;
                string nombreFuncion = $"sp_abm{_tableName}";

                respuesta = await this._conexion.AbmObjeto<T>(nombreFuncion, AbmAccion.ELIMINAR_BAJA, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaDB> Guardar(T datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = $"sp_abm{_tableName}";

                respuesta = await _conexion.AbmObjeto<T>(nombreFuncion, AbmAccion.GUARDAR, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaDB> Modificar(T datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = $"sp_abm{_tableName}";
         
                respuesta = await this._conexion.AbmObjeto<T>(nombreFuncion, AbmAccion.MODIFICAR, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
