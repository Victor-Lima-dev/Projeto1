using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Areas.Admin.Models;
using ResolverQuestao.Context;

namespace ResolverQuestao.Areas.Admin.Controllers
{
    [Route("[controller]")]

    
    [Authorize(Roles = "Admin")]

    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        //add o contexto

        private readonly UserManager<IdentityUser> _userManager;

        //roles

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly AppDbContext _context;

      

        public AdminController(ILogger<AdminController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

        }
    

        [HttpGet("Index")]
        public IActionResult Index()
        {

            var users = _userManager.Users.ToList();

            var roles = _roleManager.Roles.ToList();

            //criar a view model

            var usuarios = new List<Usuario>();

            foreach (var user in users)
            {
                var usuario = new Usuario
                {
                    UserName = user.UserName,
                    Id = user.Id
                };

                var role = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);

                if (role != null)
                {
                    var roleNome = _context.Roles.FirstOrDefault(x => x.Id == role.RoleId);

                    usuario.Role = roleNome.Name;
                }

                usuarios.Add(usuario);
            }


            return View(usuarios);
        }


        [HttpPost("DefinirRole")]
        public async Task<IActionResult> DefinirRoleAsync(string Id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == Id);

            //verificar se o usuario existe

            if (user == null)
            {
                return NotFound();
            }

           //verificar qual a role do usuario

            var role = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);

            if (role != null)
            {
                var roleNome = _context.Roles.FirstOrDefault(x => x.Id == role.RoleId);

                //remover a role

                await _userManager.RemoveFromRoleAsync(user, roleNome.Name);

                //adicionar a role

                await _userManager.AddToRoleAsync(user, "SuperUser");

                //salvar no banco de dados

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost("DeletarUsuario")]

         public async Task<IActionResult> DeletarUsuario(string Id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == Id);

            //verificar se o usuario existe

            if (user == null)
            {
                return NotFound();
            }

            //deletar o usuario

            await _userManager.DeleteAsync(user);

            //apagar o perfil dele em Usuarios

            var usuarioBancoDados = _context.UsuarioBancoDados.FirstOrDefault(x => x.UsuarioGuidId.ToString() == Id);

            if (usuarioBancoDados != null)
            {
                _context.UsuarioBancoDados.Remove(usuarioBancoDados);

                await _context.SaveChangesAsync();
            }



        


            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}