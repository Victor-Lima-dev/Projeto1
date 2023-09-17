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

    [HttpGet("Index")]
        public IActionResult Index()
        {
            var exercicios = _context.Exercicios.ToList();
            var listaExercicios = _context.ListaExercicios.ToList();
            var feedbacks = _context.FeedBacks.ToList();

            
            return View(feedbacks);
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

            if(listaExercicio == null)
            {
                return NotFound();
            }
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == exercicioId);

            if(exercicio == null)
            {
                return NotFound();
            }

            var feedBack = new FeedBack
            {
                Descricao = descricao,
                ExercicioId = exercicioId,
                Exercicio = exercicio
            };

            _context.FeedBacks.Add(feedBack);
            _context.SaveChanges();

            
            return RedirectToAction("ResponderSequencia","ListaExercicios", new { id = listaExercicio.ListaExercicioId, indice = indice, acertos = acertos, erros = erros });
            
        }

        //HTTP Post ("Avaliar")

        [HttpPost("Avaliar")]
        public IActionResult Avaliar(int id, string avaliacao)
        {
            
            //procurar o feedback com id

            var feedBack = _context.FeedBacks.FirstOrDefault(f => f.FeedBackId == id);

            if(feedBack == null)
            {
                return NotFound();
            }

            feedBack.Avaliada = true;

            feedBack.Avaliacao = avaliacao;

            _context.FeedBacks.Update(feedBack);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //HTTP Post ("Resolver")
        [HttpPost("Resolver")]
        public IActionResult Resolver(int id, string solucao)
        {
            //procurar o feedback com id

            var feedBack = _context.FeedBacks.FirstOrDefault(f => f.FeedBackId == id);

            if(feedBack == null)
            {
                return NotFound();
            }

            ///verificar se a avaliacao é true, se nao forn marcar como verdadeira
            ///
            if(feedBack.Avaliada == false)
            {
                feedBack.Avaliada = true;
            }

            feedBack.Resolvida = true;

            feedBack.Solucao = solucao;

            _context.FeedBacks.Update(feedBack);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}