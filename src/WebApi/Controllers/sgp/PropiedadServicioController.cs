
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
    public class PropiedadServicioController : ControllerBase
    {
        private readonly IPropiedadServicioService _propiedadServicioService;

        public PropiedadServicioController(IPropiedadServicioService propiedadServicioService)
        {
            _propiedadServicioService = propiedadServicioService;
        }

        // GET: api/PropiedadServicio
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _propiedadServicioService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/PropiedadServicio/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _propiedadServicioService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/PropiedadServicio
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] PropiedadServicio propiedadServicio)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedadServicio.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadServicioService.Guardar(propiedadServicio);
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

        // PUT api/PropiedadServicio
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] PropiedadServicio propiedadServicio)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedadServicio.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadServicioService.Modificar(propiedadServicio);
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


        // DELETE api/PropiedadServicio/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var propiedadServicio = await _propiedadServicioService.BuscarPorNumSec(codigo);
            propiedadServicio.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadServicioService.Eliminar(propiedadServicio);
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

