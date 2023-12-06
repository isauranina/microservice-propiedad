using Application.DTOs.Administracion;
using Application.Interfaces.ISecurity;
using Application.Services.Administracion.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.Administracion
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LoginController : ControllerBase
    { 
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly LoginFactory _loginFactory;
        public LoginController(IJwtTokenGenerator jwtTokenGenerator, LoginFactory loginFactory)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _loginFactory = loginFactory;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(UsuarioDto usuarioDto)
        {
            // string ipCliente = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            string ipCliente = Request.Headers["x-forwarded-for"].ToString();
            var usuario = await _loginFactory.GetLoginService().ValidateUser(usuarioDto);
            if (usuario == null)
            {
                return BadRequest(new { error = "auth-001", message = "Usuario o contraseña invalidos" });
            }

            var token = await _jwtTokenGenerator.CreateToken(usuario);
            var userLogin = new
            {
                usuario = usuario.cuenta,
                nombre_usuario = usuario.nombre,
                rol = usuario.rol

            };
            var respuesta = new
            {
                token = token,
                usuario = userLogin
            };
            return Ok(respuesta);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("populate")]
        [HttpGet]
        public async Task<ObjectResult> populate()
        {

            var cuenta = User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var usuario = await _loginFactory.GetLoginService().Populate(cuenta);

            var token = await _jwtTokenGenerator.CreateToken(usuario);
            var userLogin = new
            {
                usuario = usuario.cuenta,
                nombre_usuario = usuario.nombre,
                rol = usuario.rol

            };
            var respuesta = new
            {
                token = token,
                usuario = userLogin
            };
            return Ok(respuesta);
        }
    }
}
