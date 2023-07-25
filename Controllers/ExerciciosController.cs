using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [Route("[controller]")]
    public class ExerciciosController : Controller
    {
        private readonly ILogger<ExerciciosController> _logger;

        private readonly AppDbContext _context;

        public ExerciciosController(ILogger<ExerciciosController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("Index")]
        public IActionResult Index()
        {
            //deixar uma lista com os exercicios

            var exercicios = _context.Exercicios.ToList();

            return View(exercicios);
        }


        //HTTP GET - CREATE
        [HttpGet("Create")]
        public IActionResult Create()
        {
            //enviar um exercicio vazio para a view
            var exercicio = new Exercicio();
            return View(exercicio);
        }

        //HTTP POST - CREATE
        [HttpPost("Create")]
        public IActionResult Create(Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Exercicios.Add(exercicio);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }


            // Adiciona uma mensagem de erro para cada propriedade inválida
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].ValidationState == ModelValidationState.Invalid)
                {
                    ModelState.AddModelError(key, $"O valor de {key} é inválido.");
                }
            }
            // Retorna a mesma view com os erros
            return View(exercicio);


        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}