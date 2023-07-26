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
                if(exercicio.Explicacao == null)
                {
                    exercicio.Explicacao = "";
                }

                if(exercicio.MaterialSuporte == null)
                {
                    exercicio.MaterialSuporte = "";
                }
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


        //HTTP GET - Details

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            //buscar o exercicio no banco de dados, include alternativas

            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //verificar se o exercicio existe
            if (exercicio == null)
            {
                return NotFound();
            }

            //lista de todas as alternativas
            var alternativas = _context.Alternativas.ToList();


            //enviar o exercicio para a view
            return View(exercicio);
        }



        //HTTP GET - Edit
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            //buscar o exercicio no banco de dados
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //verificar se o exercicio existe
            if (exercicio == null)
            {
                return NotFound();
            }

            //alternativas
            var alternativas = _context.Alternativas.ToList();

            //enviar o exercicio para a view
            return View(exercicio);
        }

        //HTTP POST - Edit
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Exercicio exercicio)
        {


            //verificar se tem alguma alternativa com texto vazio, se tiver, excluir ela

            var alternativas = exercicio.Alternativas.ToList();

            foreach (var alternativa in alternativas)
            {
                if (alternativa.Texto == null || alternativa.Texto == "")
                {
                    _context.Alternativas.Remove(alternativa);
                }

            }



            _context.Exercicios.Update(exercicio);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }

        //HTTP GET - Delete
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            //buscar o exercicio no banco de dados
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //apagar o exercicio
            _context.Exercicios.Remove(exercicio);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        //HTTP GET - Resolver Questao / id

        [HttpGet("ResolverQuestao/{id}")]
        public IActionResult ResolverQuestao(int id)
        {
            //buscar o exercicio no banco de dados
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //verificar se o exercicio existe
            if (exercicio == null)
            {
                return NotFound();
            }

            //lista de todas as alternativas
            var alternativas = _context.Alternativas.ToList();


            return View(exercicio);
        }


        //HTTP POST - Resolver Questao / id

        [HttpPost("ResolverQuestao/{id}")]
        public IActionResult ResolverQuestao(int id, string resposta)
        {
            //procurar o exercicio
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //lista de todas as alternativas

            var alternativas = _context.Alternativas.ToList();

            //verificar se o exercicio existe
            if (exercicio == null)
            {
                return NotFound();
            }

            //verificar a resposta

            if (resposta == exercicio.Resposta)
            {
                ViewBag.Resposta = true;
            }
            else
            {
                ViewBag.Resposta = false;
            }

              //verificar se o exercicio possui explicaçao
            if (exercicio.Explicacao == null || exercicio.Explicacao == "")
            {
                ViewBag.Explicacao = false;
            }
            else
            {
                ViewBag.Explicacao = true;
            }

            //verificar se o exercicio possui referencia
            if (exercicio.MaterialSuporte == null || exercicio.MaterialSuporte  == "")
            {
                ViewBag.MaterialSuporte = false;
            }
            else
            {
                ViewBag.MaterialSuporte = true;
            }


            return View(exercicio);
        }












        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}