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
using Microsoft.AspNetCore.Authorization;

namespace ResolverQuestao.Controllers
{
    [Authorize]
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

            //pegar as listas do usuario logado
            var listasUsuario = new List<ListaExercicio>();
            listasUsuario = listas.Where(l => l.UsuarioId == User.Identity.Name).ToList();


            return View(listasUsuario);
        }

        //HTTP GET - CREATE
        [HttpGet("Create")]
        public IActionResult Create()
        {
            //lista de exercicios
            var exercicios = _context.Exercicios.ToList();

            //filtrar os exercicios do usuario logado
            var exerciciosUsuario = exercicios.Where(e => e.UsuarioId == User.Identity.Name).ToList();

            var listaViewModel = new ListaExerciciosViewModel();

            listaViewModel.Exercicios = exerciciosUsuario;



            return View(listaViewModel);
        }


        //HTTP POST - CREATE
        [HttpPost("Create")]
        public IActionResult Create(ListaExerciciosViewModel listaViewModel)
        {
            var exercicios = _context.Exercicios.ToList();

            //colocar o id do usuario logado na lista

            listaViewModel.ListaExercicio.UsuarioId = User.Identity.Name;


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
            listaCriada.UsuarioId = listaViewModel.ListaExercicio.UsuarioId;

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

            //fazer um foreach em que adiciona todos os exerciciosSelecioanadosEdit em exercicios Selecionados
            foreach (var item in exerciciosSelecionadosEdit)
            {
                exerciciosSelecionados.Add(item);
            }
            


            //limpar a lista de exercicios
            listaExercicio.Exercicios.Clear();

            var exerciciosSelecionadosFinal = exerciciosSelecionados.Distinct().ToList();




            //adicionar ao listaExercicio os exercicios selecionados
            foreach (var item in exerciciosSelecionados)
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


            //viewbag com o id do exercicio
            ViewBag.IdExercicio = exercicio.ExercicioId;



            return View(listaExercicio);
        }



        //Responder Lista
        [HttpGet("ResponderSequencia/{id}")]
        public IActionResult ResponderSequencia(int? id, int indice, int acertos, int erros)
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

            listaExercicio.IndiceExercicio = indice;

            if (acertos == 0)
            {
                ViewBag.Acertos = 0;
            }
            else
            {
                ViewBag.Acertos = acertos;
            }

            if (erros == 0)
            {
                ViewBag.Erros = 0;
            }
            else
            {
                ViewBag.Erros = erros;
            }


            return View(listaExercicio);
        }

        [HttpPost("ResponderSequencia/{id}")]
        public IActionResult ResponderSequencia(int id, string resposta, int indice, int acertos, int erros)
        {





            //procurar o exercicio
            var exercicio = _context.Exercicios.FirstOrDefault(e => e.ExercicioId == id);

            //procurar qual lista ele pertence
            var listaExercicio = _context.ListaExercicios.Include(l => l.Exercicios).FirstOrDefault(l => l.Exercicios.Contains(exercicio));

            //lista de todas as alternativas

            listaExercicio.IndiceExercicio = indice;

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
                acertos++;
            }
            else
            {
                ViewBag.Resposta = false;
                erros++;
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


            //viewbag com o id do exercicio
            ViewBag.IdExercicio = exercicio.ExercicioId;
            ViewBag.Acertos = acertos;
            ViewBag.Erros = erros;



            return View(listaExercicio);
        }

        //HTTP POST - ProximoExercicio
        [HttpPost("ProximoExercicio")]
        public IActionResult ProximoExercicio(int id, int indice, int acertos, int erros)
        {
            int proximoIndice = indice + 1;
            //procurar a lista com id
            var listaExercicio = _context.ListaExercicios
                                        .Include(l => l.Exercicios)
                                        .ThenInclude(e => e.Alternativas)
                                        .FirstOrDefault(l => l.ListaExercicioId == id);

            //redirecionar para o exercicio com o proximo indice
            ViewBag.Acertos = acertos;

            return RedirectToAction("ResponderSequencia", new { id = listaExercicio.ListaExercicioId, indice = proximoIndice, acertos = acertos, erros = erros });
        }

        //HTTP POST - VoltarExercicio
        [HttpPost("VoltarExercicio")]
        public IActionResult VoltarExercicio(int id, int indice, int acertos, int erros)
        {
            int proximoIndice = indice - 1;
            //procurar a lista com id
            var listaExercicio = _context.ListaExercicios
                                        .Include(l => l.Exercicios)
                                        .ThenInclude(e => e.Alternativas)
                                        .FirstOrDefault(l => l.ListaExercicioId == id);

            //redirecionar para o exercicio com o proximo indice
            ViewBag.Acertos = acertos;
            return RedirectToAction("ResponderSequencia", new { id = listaExercicio.ListaExercicioId, indice = proximoIndice, acertos = acertos, erros = erros });
        }

        //HTTP POST - SelecionarExercicio
        [HttpPost("SelecionarExercicio")]
        public IActionResult SelecionarExercicio(int id, int indice, int acertos, int erros)
        {
            //procurar a lista com id
            var listaExercicio = _context.ListaExercicios
                                        .Include(l => l.Exercicios)
                                        .ThenInclude(e => e.Alternativas)
                                        .FirstOrDefault(l => l.ListaExercicioId == id);

            //redirecionar para o exercicio com o proximo indice

            return RedirectToAction("ResponderSequencia", new { id = listaExercicio.ListaExercicioId, indice = indice, acertos = acertos, erros = erros });
        }


        //metodo para procurar por tipo
        [HttpGet("ProcurarPorTipo")]
        public IActionResult ProcurarPorTipo(string tipo)
        {
            //pegar as listas do usuario logado
            var listas = new List<ListaExercicio>();
            listas = _context.ListaExercicios.Include(l => l.Exercicios).ToList();

            var listasUsuario = listas.Where(l => l.UsuarioId == User.Identity.Name).ToList();

            //verificar se o tipo é nulo
            if (tipo == null)
            {
                return View("Index", listasUsuario);
            }

            //verificar se o tipo é vazio
            if (tipo == "")
            {
                return View("Index", listasUsuario);
            }

            //pegar as listas com o tipo
            var listasPorTipo = listasUsuario.Where(l => l.Tipo == tipo).ToList();

            return View("Index", listasPorTipo);
        }




        //metodo para procurar por tipo
        [HttpGet("ProcurarPorTipoExercicios")]
        public IActionResult ProcurarPorTipoExercicios(string tipo)
        {
            var listaViewModel = new ListaExerciciosViewModel();
            //pegar os exercicios do usuario logado
            var exercicios = new List<Exercicio>();
            exercicios = _context.Exercicios.ToList();
            var exerciciosUsuario = exercicios.Where(e => e.UsuarioId == User.Identity.Name).ToList();

            //filtrar pelo tipo
            if (tipo == null)
            {
                return View("Create", listaViewModel);
            }
            var exerciciosFiltrados = exerciciosUsuario.Where(e => e.Tipo == tipo).ToList();


            listaViewModel.Exercicios = exerciciosFiltrados;

            return View("Create", listaViewModel);


        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}