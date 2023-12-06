
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
    public class EstadoPropiedadController : ControllerBase
    {
        private readonly IEstadoPropiedadService _estadoPropiedadService;

        public EstadoPropiedadController(IEstadoPropiedadService estadoPropiedadService)
        {
            _estadoPropiedadService = estadoPropiedadService;
        }

        // GET: api/EstadoPropiedad
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _estadoPropiedadService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/EstadoPropiedad/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _estadoPropiedadService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/EstadoPropiedad
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] EstadoPropiedad estadoPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPropiedadService.Guardar(estadoPropiedad);
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

        // PUT api/EstadoPropiedad
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] EstadoPropiedad estadoPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPropiedadService.Modificar(estadoPropiedad);
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


        // DELETE api/EstadoPropiedad/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var estadoPropiedad = await _estadoPropiedadService.BuscarPorNumSec(codigo);
            estadoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPropiedadService.Eliminar(estadoPropiedad);
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

