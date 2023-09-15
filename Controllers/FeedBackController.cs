using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [Route("[controller]")]
    public class FeedBackController : Controller
    {
        private readonly ILogger<FeedBackController> _logger;

        private readonly AppDbContext _context;

        public FeedBackController(ILogger<FeedBackController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        //HTTP Post ("Registrar")

        [HttpPost("Registrar")]

        public IActionResult Registrar(string descricao, int exercicioId, int id, int indice, int acertos, int erros)
        {
             //procurar a lista com id
            var listaExercicio = _context.ListaExercicios
                                        .Include(l => l.Exercicios)
                                        .ThenInclude(e => e.Alternativas)
                                        .FirstOrDefault(l => l.ListaExercicioId == id);

            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == exercicioId);

            var feedBack = new FeedBack
            {
                Descricao = descricao,
                ExercicioId = exercicioId,
                Exercicio = exercicio
            };

            // _context.FeedBacks.Add(feedBack);
            // _context.SaveChanges();

            
            return RedirectToAction("ResponderSequencia","ListaExercicios", new { id = listaExercicio.ListaExercicioId, indice = indice, acertos = acertos, erros = erros });
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}