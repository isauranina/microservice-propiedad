
using Application.Interfaces.IServices.sgp;
using Application.Services;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models;
using Domain.Models.sgp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Services.Rabbit;
using Application.Interfaces.IServices.Rabbit;
using Application.DTOs.sgp;

namespace WebApi.Controllers.sgp
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly IPropiedadService _propiedadService;
        // private readonly RabbitService _amqpService;
        private readonly IAmqpService _amqpService;

        public PropiedadController(IPropiedadService propiedadService, IAmqpService amqpService)
        {
            _propiedadService = propiedadService;
            _amqpService = amqpService;
        }

        // GET: api/Propiedad
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _propiedadService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Propiedad/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _propiedadService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Propiedad
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] Propiedad propiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedad.nsec_usuario_registro = long.Parse(nsecUsuario);          
            var respuestaBD = await _propiedadService.Guardar(propiedad);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
           // bool enviado = _amqpService.Publish(propiedad, "");
            return Ok(respuestaBD);
        }
        // POST api/Propiedad/cambiar_estado
        [HttpPost]
        [Route("cambiar_estado")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Postcambiar_estado([FromBody] VerificarPropiedad datos)
        {
            //string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            //if (nsecUsuario.Trim().Equals("0"))
            //{
            //    var respuestaError = new RespuestaError()
            //    {
            //        error = Status.Error,
            //        message = "No se pudo obtener el codigo usuario logueado"
            //    };
            //    return BadRequest(respuestaError);
            //}        

            var respuestaBD = await _propiedadService.EstadoPropiedad(datos);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            var datosBusqueda = await _propiedadService.traer_por_id(datos.num_sec);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datosBusqueda
            };
            bool enviado = _amqpService.Publish(respuesta.response, "");
            return Ok(respuesta.response);
        }

        // PUT api/Propiedad
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] Propiedad propiedad)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            propiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadService.Modificar(propiedad);
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


        // DELETE api/Propiedad/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var propiedad = await _propiedadService.BuscarPorNumSec(codigo);
            propiedad.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _propiedadService.Eliminar(propiedad);
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

