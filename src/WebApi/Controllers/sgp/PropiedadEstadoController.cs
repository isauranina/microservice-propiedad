
using Application.Interfaces.IServices.sgp;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models;
using Domain.Models.sgp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.sgp
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PropiedadEstadoController : ControllerBase
    {
        private readonly IPropiedadEstadoService _propiedadEstadoService;

        public PropiedadEstadoController(IPropiedadEstadoService propiedadEstadoService)
        {
            _propiedadEstadoService = propiedadEstadoService;
        }

        // GET: api/PropiedadEstado
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _propiedadEstadoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/PropiedadEstado/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _propiedadEstadoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/PropiedadEstado
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] PropiedadEstado propiedadEstado)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedadEstado.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadEstadoService.Guardar(propiedadEstado);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }

        // PUT api/PropiedadEstado
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] PropiedadEstado propiedadEstado)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedadEstado.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadEstadoService.Modificar(propiedadEstado);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }


        // DELETE api/PropiedadEstado/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var propiedadEstado = await _propiedadEstadoService.BuscarPorNumSec(codigo);
            propiedadEstado.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadEstadoService.Eliminar(propiedadEstado);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }

    }
}

