using System.Diagnostics;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResolverQuestao.Context;
using ResolverQuestao.Models;
using ResolverQuestao.Models.ViewModels;

namespace ResolverQuestao.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    [Authorize]
    public IActionResult Index()
    {
        var exercicios = _context.Exercicios.ToList();

        //pegar o id do usuario logado

        var usuarioLogado = User.Identity.Name;

        //contar quantos exercicios o usuario logado tem
        var contagemExercicios = exercicios.Where(e => e.UsuarioId == usuarioLogado).Count();

        //pegar a lista de exercicios do usuario logado
        var ExerciciosUsuario = exercicios.Where(e => e.UsuarioId == usuarioLogado).ToList();

        //enviar o numero em uma viewBag
        ViewBag.ContagemExercicios = contagemExercicios;

        var listas = _context.ListaExercicios.ToList();

        //contar as listas do usuario logado

        var contagemListas = listas.Where(l => l.UsuarioId == usuarioLogado).Count();

        //pegar as listas de exercicios do usuario logado

        var ListasUsuario = listas.Where(l => l.UsuarioId == usuarioLogado).ToList();

        //criar a view model ListaExerciciosViewModel

        var listaExercicioHomeViewModel = new ListaExercicioHomeViewModel
        {
            ListaExercicios = ListasUsuario,
            Exercicios = ExerciciosUsuario
        };

        //enviar o numero em uma viewBag
        ViewBag.ContagemListas = contagemListas;
        
        return View(listaExercicioHomeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



}
