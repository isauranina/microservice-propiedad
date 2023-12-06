
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
    public class TipoPropiedadController : ControllerBase
    {
        private readonly ITipoPropiedadService _tipoPropiedadService;

        public TipoPropiedadController(ITipoPropiedadService tipoPropiedadService)
        {
            _tipoPropiedadService = tipoPropiedadService;
        }

        // GET: api/TipoPropiedad
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _tipoPropiedadService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/TipoPropiedad/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _tipoPropiedadService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/TipoPropiedad
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] TipoPropiedad tipoPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPropiedadService.Guardar(tipoPropiedad);
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

        // PUT api/TipoPropiedad
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] TipoPropiedad tipoPropiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPropiedadService.Modificar(tipoPropiedad);
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


        // DELETE api/TipoPropiedad/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoPropiedad = await _tipoPropiedadService.BuscarPorNumSec(codigo);
            tipoPropiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPropiedadService.Eliminar(tipoPropiedad);
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

