using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ListaRegistrosController : Controller
    {
        private readonly ILogger<ListaRegistrosController> _logger;

        private readonly AppDbContext _context;

        public ListaRegistrosController(ILogger<ListaRegistrosController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //HTTP GET Index
        [HttpGet("index")]
        public IActionResult Index()
        {
            var listaExercicios = _context.ListaExercicios.ToList();
            var registros = _context.ListaRegistros.ToList();

            //pegar o id do usuario logado
            var usuarioId = User.Identity.Name;

            //pegar os registros do usuario logado
            var registrosUsuario = registros.FindAll(registro => registro.UsuarioId == usuarioId);

            //invertendo a lista para mostrar os ultimos registros primeiro
            registrosUsuario.Reverse();

            return View(registrosUsuario);

      
        }

        //HTTP GET ProcurarPorTipo

        [HttpGet("procurarPorTipo")]
        public IActionResult ProcurarPorTipo(string tipo)
        {
            var listaExercicios = _context.ListaExercicios.ToList();
            var registros = _context.ListaRegistros.ToList();

            //pegar o id do usuario logado
            var usuarioId = User.Identity.Name;

            //pegar os registros do usuario logado
            var registrosUsuario = registros.FindAll(registro => registro.UsuarioId == usuarioId);

            //pegar os registros do tipo passado
            var registrosTipo = registrosUsuario.FindAll(registro => registro.TituloLista == tipo);

            //invertendo a lista para mostrar os ultimos registros primeiro
            registrosTipo.Reverse();

            return View("Index", registrosTipo);
        }

         //HTTP ProcurarPorData
        [HttpGet("procurarPorData")]
        public IActionResult ProcurarPorData(DateTime data)
        {
            var listaExercicios = _context.ListaExercicios.ToList();
            var registros = _context.ListaRegistros.ToList();

            //pegar o id do usuario logado
            var usuarioId = User.Identity.Name;

            //pegar os registros do usuario logado
            var registrosUsuario = registros.FindAll(registro => registro.UsuarioId == usuarioId);

            //pegar os registros da data passada, comparar só com o dia, mes e ano, nao comparar com a hora
            var registrosData = registrosUsuario.FindAll(registro => registro.DataRegistro.Day == data.Day && registro.DataRegistro.Month == data.Month && registro.DataRegistro.Year == data.Year).ToList();

            
            //invertendo a lista para mostrar os ultimos registros primeiro
            registrosData.Reverse();

            return View("Index", registrosData);
        }

       [HttpGet("procurarPorMateria")]
       public IActionResult ProcurarPorMateria(string materia)
       {
              var listaExercicios = _context.ListaExercicios.ToList();
              var registros = _context.ListaRegistros.ToList();
    
              //pegar o id do usuario logado
              var usuarioId = User.Identity.Name;
    
              //pegar os registros do usuario logado
              var registrosUsuario = registros.FindAll(registro => registro.UsuarioId == usuarioId);
    
              //pegar os registros da materia passada
              var registrosMateria = registrosUsuario.FindAll(registro => registro.ListaExercicio.Tipo == materia);
    
              //invertendo a lista para mostrar os ultimos registros primeiro
              registrosMateria.Reverse();
    
              return View("Index", registrosMateria);

       }



        //HTTP POST Registrar
        [HttpPost("registrar")]
        public IActionResult Registrar(int id, int acertos, int erros)
        {
            //listar todas as listas de exercicios
            var listaExercicios = _context.ListaExercicios.ToList();
            //pegar a lista de exercicio que tem o id passado
            var listaExercicio = listaExercicios.Find(lista => lista.ListaExercicioId == id);

            //pegar o titulo da lista de exercicio
            var tituloLista = listaExercicio.Titulo;

            //usuario id vai ser o id do usuario logado
            var usuarioId = User.Identity.Name;

            var registro = new ListaRegistro
            {
                ListaExercicioId = id,
                UsuarioId = usuarioId,
                DataRegistro = DateTime.Now,
                Acertos = acertos,
                Erros = erros,
                TituloLista = tituloLista
            };

            _context.ListaRegistros.Add(registro);
            _context.SaveChanges();

            return RedirectToAction("Index", "ListaExercicios");
        }

        //HTTP POST DELETE
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var registro = _context.ListaRegistros.Find(id);
            _context.ListaRegistros.Remove(registro);
            _context.SaveChanges();
            return RedirectToAction("Index", "ListaRegistros");
        }

       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}