using Microsoft.AspNetCore.Mvc;
using Developers.Models;
using Developers.Recursos;
using Developers.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Developers.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public InicioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Password = Utilidade.EncriptarContrasena(modelo.Password);

            Usuario UsuarioCreado = await _usuarioServicio.SaveUsuario(modelo);

            if (UsuarioCreado.Id > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string Correo, string Password)
        {
            Usuario Usuario_Encontrado = await _usuarioServicio.GetUsuario(Correo, Utilidade.EncriptarContrasena(Password));

            if (Usuario_Encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Usuario_Encontrado.Nombre)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
        }
    }
}
