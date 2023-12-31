using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Models;




namespace ResolverQuestao.Controllers
{

    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;


        public UsuariosController(ILogger<UsuariosController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public IActionResult Index()
        {
            return View();
        }

        //HTTP GET login

        [HttpGet("login")]
        public IActionResult Login(string url)
        {

            var usuario = new Usuario
            {
                ReturnUrl = url
            };

            return View(usuario);
        }

        //HTTp POST login
        [HttpPost("login")]
        public IActionResult Login(Usuario usuario)
        {

            var user = _userManager.FindByNameAsync(usuario.Username).Result;

            if (user != null)

            {
                var result = _signInManager.PasswordSignInAsync(user, usuario.Password, false, false).Result;

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(usuario.ReturnUrl))
                    {
                        return Redirect(usuario.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ViewBag.Erro = "Usuário ou senha inválidos";
            return View();
        }



        //HTTP GET Registrar

        [HttpGet("registrar")]
        public IActionResult Registrar()
        {
            return View();
        }

        //HTTP POST Registrar
        [HttpPost("registrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            var user = new IdentityUser() { UserName = usuario.Username };
            var result = await _userManager.CreateAsync(user, usuario.Password);

            //atribuir a role de User para o usuario


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                //logar o usuario
                await _signInManager.SignInAsync(user, false);


                return RedirectToAction("CriarPerfil", "Perfil");
            }


            if (result.Errors != null && result.Errors.Any())
            {
                ViewBag.Erro = result.Errors.FirstOrDefault().Description;
            }
            else
            {
                ViewBag.Erro = "Erro Inesperado";
            }

            return View(usuario);

        }

        //HTTP GET Acesso Negado

        [HttpGet("acessoNegado")]
        public IActionResult AcessoNegado()
        {
            return View();
        }




        //HTTP Post LOUGOUT

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}