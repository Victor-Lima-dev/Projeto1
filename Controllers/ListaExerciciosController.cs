using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models.ViewModels;

using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [Route("[controller]")]
    public class ListaExerciciosController : Controller
    {
        private readonly ILogger<ListaExerciciosController> _logger;

        private readonly AppDbContext _context;

        public ListaExerciciosController(ILogger<ListaExerciciosController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        //HTTP GET - INDEX
        [HttpGet("Index")]
        public IActionResult Index()
        {
            //deixar uma lista com os exercicios
            var exercicios = _context.Exercicios.Include(e => e.ListaExercicios).ToList();
            var listas = _context.ListaExercicios.Include(l => l.Exercicios).ToList();
            return View(listas);
        }

        //HTTP GET - CREATE
        [HttpGet("Create")]
        public IActionResult Create()
        {
            //lista de exercicios
            var exercicios = _context.Exercicios.ToList();
            var listaViewModel = new ListaExerciciosViewModel();

            listaViewModel.Exercicios = exercicios;

            return View(listaViewModel);
        }


        //HTTP POST - CREATE
        [HttpPost("Create")]
        public IActionResult Create(ListaExerciciosViewModel listaViewModel)
        {
            var exercicios = _context.Exercicios.ToList();

            var listaCriada = new ListaExercicio();

            var ExerciciosSelecionados = listaViewModel.ExerciciosSelecionados;

            //passar para a listaCriada
            foreach (var item in ExerciciosSelecionados)
            {
                var exercicio = _context.Exercicios.Find(item);
                listaCriada.Exercicios.Add(exercicio);
            }

            //adicionar o titulo,tipo,descriçao e MaterialSuporte
            listaCriada.Titulo = listaViewModel.ListaExercicio.Titulo;
            listaCriada.Tipo = listaViewModel.ListaExercicio.Tipo;
            listaCriada.Descricao = listaViewModel.ListaExercicio.Descricao;
            listaCriada.MaterialSuporte = listaViewModel.ListaExercicio.MaterialSuporte;

            _context.ListaExercicios.Add(listaCriada);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


        //HTTP GET - Details
        [HttpGet("Details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.ListaExercicioId == id);

            if (listaExercicio == null)
            {
                return NotFound();
            }

            //verificar se possui material de suporte

            if (listaExercicio.MaterialSuporte == null)
            {
                ViewBag.MaterialSuporte = false;
            }
            else
            {
                ViewBag.MaterialSuporte = true;
            }

            return View(listaExercicio);
        }



        //HTTP GET - Delete
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.ListaExercicioId == id);

            if (listaExercicio == null)
            {
                return NotFound();
            }

            //remover da lista
            _context.ListaExercicios.Remove(listaExercicio);
            _context.SaveChanges();
            //redirecionar para a pagina index
            return RedirectToAction("Index");
        }



        //HTTP GET - Edit

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.ListaExercicioId == id);

            if (listaExercicio == null)
            {
                return NotFound();
            }

            //verificar se possui material de suporte

            if (listaExercicio.MaterialSuporte == null)
            {
                ViewBag.MaterialSuporte = false;
            }
            else
            {
                ViewBag.MaterialSuporte = true;
            }


            //criar a listaExercicioVIewModel

            var listaExercicioViewModel = new ListaExerciciosViewModel();

            //exercicios
            var todosExercicios = _context.Exercicios.ToList();

            //exercicios da lista

            var exercicios = listaExercicio.Exercicios.ToList();

            //adicionar os outros atributos 
            listaExercicioViewModel.ListaExercicio = listaExercicio;
            listaExercicioViewModel.TodosExercicios = todosExercicios;
            listaExercicioViewModel.Exercicios = exercicios;

            return View(listaExercicioViewModel);
        }


        //HTTP POST - Edit
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(ListaExerciciosViewModel listaViewModel)
        {

            var exercicios = _context.Exercicios.ToList();

            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.ListaExercicioId == listaViewModel.ListaExercicio.ListaExercicioId);




            var exerciciosSelecionados = listaViewModel.ExerciciosSelecionados;

            var exerciciosSelecionadosEdit = listaViewModel.ExerciciosSelecionadosEdit;

            //unir as duas listas de exercicios selecionados e remover os duplicados
            var exerciciosSelecionadosFinal = exerciciosSelecionados.Union(exerciciosSelecionadosEdit).ToList();


            //limpar a lista de exercicios
            listaExercicio.Exercicios.Clear();




            //adicionar ao listaExercicio os exercicios selecionados
            foreach (var item in exerciciosSelecionadosFinal)
            {
                var exercicio = _context.Exercicios.Find(item);
                listaExercicio.Exercicios.Add(exercicio);
            }

            //update dos outros atributos
            _context.ListaExercicios.Update(listaExercicio);

            _context.SaveChanges();





            return RedirectToAction("Index");

        }

        //Responder Lista
        [HttpGet("ResponderLista/{id}")]
        public IActionResult ResponderLista(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var alternativas
            var alternativas = _context.Alternativas.ToList();




            var listaExercicio = _context.ListaExercicios
                                        .Include(l => l.Exercicios)
                                        .ThenInclude(e => e.Alternativas)
                                        .FirstOrDefault(l => l.ListaExercicioId == id);


            if (listaExercicio == null)
            {
                return NotFound();
            }

            //verificar se possui material de suporte

            if (listaExercicio.MaterialSuporte == null)
            {
                ViewBag.MaterialSuporte = false;
            }
            else
            {
                ViewBag.MaterialSuporte = true;
            }

            return View(listaExercicio);
        }



        //HTTP POST - Resolver Questao / id

        [HttpPost("ResponderLista/{id}")]
        public IActionResult ResponderLista(int id, string resposta)
        {
            //procurar o exercicio
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //procurar qual lista ele pertence
            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.Exercicios.Contains(exercicio));

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

            ViewBag.IdExercicio= id;


            return View(listaExercicio);
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}