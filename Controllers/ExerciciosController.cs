using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models;
using ResolverQuestao.Models.ViewModels;

namespace ResolverQuestao.Controllers
{
    [Authorize]
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

            //pegar os exercicios do usuario logado

            var exerciciosUsuario = new List<Exercicio>();

            exerciciosUsuario = exercicios.Where(e => e.UsuarioId == User.Identity.Name).ToList();

            //inverter lista
            exerciciosUsuario.Reverse();

            var tipos = new List<string>();

            foreach (var exercicio in exerciciosUsuario)
            {
                if (!tipos.Contains(exercicio.Tipo))
                {
                    tipos.Add(exercicio.Tipo);
                }
            }


            var viewModel = new IndexExerciciosViewModel
            {
                Exercicios = exerciciosUsuario,
                Tipos = tipos
            };


            return View(viewModel);
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


            //colocar o id do usuario logado no exercicio

            exercicio.UsuarioId = User.Identity.Name;


            if (exercicio.Explicacao == null)
            {
                exercicio.Explicacao = "";
            }

            if (exercicio.MaterialSuporte == null)
            {
                exercicio.MaterialSuporte = "";
            }
            _context.Exercicios.Add(exercicio);
            _context.SaveChanges();

            return RedirectToAction("Index");


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
            if (exercicio.MaterialSuporte == null || exercicio.MaterialSuporte == "")
            {
                ViewBag.MaterialSuporte = false;
            }
            else
            {
                ViewBag.MaterialSuporte = true;
            }


            return View(exercicio);
        }

        //metodo para procurar por tipo
        [HttpGet("ProcurarPorTipo")]
        public IActionResult ProcurarPorTipo(string tipo)
        {
            //pegar a lista de exercicios do usuario
            var exercicios = _context.Exercicios.ToList();

            //pegar os exercicios do usuario logado

            var exerciciosUsuario = new List<Exercicio>();

            exerciciosUsuario = exercicios.Where(e => e.UsuarioId == User.Identity.Name).ToList();

            //pegar os exercicios do tipo selecionado
            var exerciciosTipo = exerciciosUsuario.Where(e => e.Tipo == tipo).ToList();

              var tipos = new List<string>();

            foreach (var exercicio in exerciciosUsuario)
            {
                if (!tipos.Contains(exercicio.Tipo))
                {
                    tipos.Add(exercicio.Tipo);
                }
            }


            var viewModel = new IndexExerciciosViewModel
            {
                Exercicios = exerciciosTipo,
                Tipos = tipos
            };

            return View("Index", viewModel);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}