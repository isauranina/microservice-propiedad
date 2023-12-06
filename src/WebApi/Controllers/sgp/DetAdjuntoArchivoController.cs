
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
    public class DetAdjuntoArchivoController : ControllerBase
    {
        private readonly IDetAdjuntoArchivoService _detAdjuntoArchivoService;

        public DetAdjuntoArchivoController(IDetAdjuntoArchivoService detAdjuntoArchivoService)
        {
            _detAdjuntoArchivoService = detAdjuntoArchivoService;
        }

        // GET: api/DetAdjuntoArchivo
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _detAdjuntoArchivoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/DetAdjuntoArchivo/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _detAdjuntoArchivoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/DetAdjuntoArchivo
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] DetAdjuntoArchivo detAdjuntoArchivo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detAdjuntoArchivo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoArchivoService.Guardar(detAdjuntoArchivo);
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

        // PUT api/DetAdjuntoArchivo
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] DetAdjuntoArchivo detAdjuntoArchivo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detAdjuntoArchivo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoArchivoService.Modificar(detAdjuntoArchivo);
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


        // DELETE api/DetAdjuntoArchivo/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detAdjuntoArchivo = await _detAdjuntoArchivoService.BuscarPorNumSec(codigo);
            detAdjuntoArchivo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoArchivoService.Eliminar(detAdjuntoArchivo);
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

