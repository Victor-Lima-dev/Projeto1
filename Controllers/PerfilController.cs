using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ResolverQuestao.Models;
using Microsoft.AspNetCore.Authorization;


namespace ResolverQuestao.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PerfilController : Controller
    {
        private readonly ILogger<PerfilController> _logger;

        private readonly AppDbContext _context;

        public PerfilController(ILogger<PerfilController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("CriarPerfil")]
        public IActionResult CriarPerfil()
        {
            var usuarioLogadoId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            //verificar se o usuario já tem um perfil

            var usuario = _context.UsuarioBancoDados.FirstOrDefault(u => u.UsuarioGuidId == Guid.Parse(usuarioLogadoId));

            if (usuario != null)
            {
                return RedirectToAction("EditarPerfil");
            }

            var usuarioBase = new UsuarioBancoDados
            {
                UsuarioGuidId = Guid.Parse(usuarioLogadoId)
            };

            return View(usuarioBase);
        }

        [HttpPost("CriarPerfil")]

        public async Task<IActionResult> CriarPerfil(UsuarioBancoDados usuario)
        {

            _context.Add(usuario);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }

        [HttpGet("EditarPerfil")]
        public IActionResult EditarPerfil()
        {    
            

            Guid id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            var usuario = _context.UsuarioBancoDados.FirstOrDefault(u => u.UsuarioGuidId == id);

            //verificar se o usuario existe

            if (usuario == null)
            {
                return RedirectToAction("CriarPerfil");
            }
            return View(usuario);
        }

        [HttpPost("EditarPerfil")]
        public async Task<IActionResult> EditarPerfil(UsuarioBancoDados usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("DetalhesPerfil")]

        public IActionResult DetalhesPerfil()
        {
            Guid id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var usuario = _context.UsuarioBancoDados.FirstOrDefault(u => u.UsuarioGuidId == id);

            //verificar se o usuario existe

            if (usuario == null)
            {
                return RedirectToAction("CriarPerfil");
            }

            

            return View(usuario);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}