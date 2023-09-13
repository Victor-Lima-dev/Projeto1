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

            var usuario = new UsuarioBancoDados
            {
                UsuarioGuidId = Guid.Parse(usuarioLogadoId)
            };

            return View(usuario);
        }

        [HttpPost("CriarPerfil")]

        public async Task<IActionResult> CriarPerfil(UsuarioBancoDados usuario)
        {

            _context.Add(usuario);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}