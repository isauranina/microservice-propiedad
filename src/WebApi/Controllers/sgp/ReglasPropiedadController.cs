
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
    public class ReglasPropiedadController : ControllerBase
    {
        private readonly IReglasPropiedadService _reglasPropiedadService;

        public ReglasPropiedadController(IReglasPropiedadService reglasPropiedadService)
        {
            _reglasPropiedadService = reglasPropiedadService;
        }

        // GET: api/ReglasPropiedad
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _reglasPropiedadService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/ReglasPropiedad/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _reglasPropiedadService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/ReglasPropiedad
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] ReglasPropiedad reglasPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            reglasPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _reglasPropiedadService.Guardar(reglasPropiedad);
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

        // PUT api/ReglasPropiedad
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] ReglasPropiedad reglasPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            reglasPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _reglasPropiedadService.Modificar(reglasPropiedad);
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


        // DELETE api/ReglasPropiedad/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var reglasPropiedad = await _reglasPropiedadService.BuscarPorNumSec(codigo);
            reglasPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _reglasPropiedadService.Eliminar(reglasPropiedad);
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

