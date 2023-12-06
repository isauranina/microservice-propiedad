
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
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;

        public CiudadController(ICiudadService ciudadService)
        {
            _ciudadService = ciudadService;
        }

        // GET: api/Ciudad
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _ciudadService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Ciudad/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _ciudadService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Ciudad
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] Ciudad ciudad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            ciudad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _ciudadService.Guardar(ciudad);
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

        // PUT api/Ciudad
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] Ciudad ciudad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            ciudad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _ciudadService.Modificar(ciudad);
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


        // DELETE api/Ciudad/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var ciudad = await _ciudadService.BuscarPorNumSec(codigo);
            ciudad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _ciudadService.Eliminar(ciudad);
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

