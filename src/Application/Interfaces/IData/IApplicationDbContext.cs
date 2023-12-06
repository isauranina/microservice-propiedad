
using Domain.Enums;
using Domain.Models.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IData
{
    public interface IApplicationDbContext
    {
        public IDbConnection ConexionPGSQL { get; }
        public Task<T> TraerObjeto<T>(string nombreProcedimiento, Hashtable parametros);
        public Task<IEnumerable<T>> TraerArrayObjeto<T>(string nombreProcedimiento, Hashtable? parametros = null);
        public Task<RespuestaDB> AbmObjeto<T>(string nombreProcedimiento, AbmAccion accion, T objeto);
        public Task<RespuestaDB> AbmEliminar<T>(string nombreProcedimiento, AbmAccion accion, long num_sec);
        public Task<RespuestaDB> EjecutarProcConParametros(string nombreProcedimiento, object parametros);
        public  Task<RespuestaDB> EjecutarProcedimiento<T>(string nombreProcedimiento, T objeto);
    }
}
