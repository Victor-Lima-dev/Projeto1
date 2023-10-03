using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResolverQuestao.Context;
using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class HistoricoController : Controller
    {
        private readonly ILogger<HistoricoController> _logger;

        private readonly AppDbContext _context;

        public HistoricoController(ILogger<HistoricoController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {

        var usuarioLogado = User.Identity.Name;
        
        var listaRegistros = _context.ListaRegistros.Where(x => x.UsuarioId == usuarioLogado).Include(x => x.ListaExercicio).ThenInclude(x => x.Exercicios).ThenInclude(x => x.Alternativas).ToList();
      
        var listaExerciciosRespondidos = new List<ListaExercicio>();

        var tiposListas = new List<string>();

        foreach (var item in listaRegistros)
        {
            tiposListas.Add(item.ListaExercicio.Tipo);
        }

        //remover os tipos de lista repetidos

        var tiposListasDistinct = tiposListas.Distinct().ToList();

        //agora vamos criar um HistoricoMateria para cada tipo de lista

        var listaHistoricoMateria = new List<HistoricoMateria>();

        foreach (var item in tiposListasDistinct)
        {
            var historicoMateria = new HistoricoMateria
            {
                NomeMateria = item
            };

            var listaRegistrosMateria = listaRegistros.Where(x => x.ListaExercicio.Tipo == item).ToList();

            var quantidadeQuestoesCorretasMateria = 0;

            foreach (var registro in listaRegistrosMateria)
            {
                quantidadeQuestoesCorretasMateria += registro.Acertos;
            }

            var quantidadeQuestoesErradasMateria = 0;

            foreach (var registro in listaRegistrosMateria)
            {
                quantidadeQuestoesErradasMateria += registro.Erros;
            }

            var quantidadeQuestoesRespondidasMateria = quantidadeQuestoesCorretasMateria + quantidadeQuestoesErradasMateria;

            var aproveitamentoMateria = quantidadeQuestoesCorretasMateria * 100 / quantidadeQuestoesRespondidasMateria;

            historicoMateria.QuantidadeQuestoesCorretas = quantidadeQuestoesCorretasMateria;

            historicoMateria.QuantidadeQuestoesErradas = quantidadeQuestoesErradasMateria;

            historicoMateria.QuantidadeQuestoesRespondidas = quantidadeQuestoesRespondidasMateria;

            historicoMateria.PorcentagemAcerto = aproveitamentoMateria.ToString() + "%";

            listaHistoricoMateria.Add(historicoMateria);
        }
            return View(listaHistoricoMateria);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}