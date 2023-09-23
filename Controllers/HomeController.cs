using System.Diagnostics;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResolverQuestao.Context;
using ResolverQuestao.Models;
using ResolverQuestao.Models.ViewModels;

using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        var todasListas = _context.ListaExercicios.ToList();

        var todosRegistros = _context.ListaRegistros.ToList();

        var todosExercicios = _context.Exercicios.ToList();

        var homeViewModel = new HomeViewModel();

        var usuarioLogado = User.Identity.Name;

        var listaRegistros = _context.ListaRegistros.Where(x => x.UsuarioId == usuarioLogado).ToList();

        //se o usuario nao tiver nenhum registro, retorna a view com o viewmodel vazio

        if (listaRegistros.Count == 0)
        {
            //enviar uma viewBag mostrando que nao tem nehuma lista respondida

            ViewBag.NenhumaListaRespondida = true;
            return View(homeViewModel);
        }



        var listaExerciciosRespondidos = new List<ListaExercicio>();

        foreach (var item in listaRegistros)
        {
            listaExerciciosRespondidos.Add(item.ListaExercicio);
        }

        int quantidadeQuestoesCorretas = 0;

        foreach (var item in listaRegistros)
        {
            quantidadeQuestoesCorretas += item.Acertos;
        }

        int quantidadeQuestoesErradas = 0;

        foreach (var item in listaRegistros)
        {
            quantidadeQuestoesErradas += item.Erros;
        }

        var quantidadeQuestoesRespondidas = quantidadeQuestoesCorretas + quantidadeQuestoesErradas;

        var aproveitamento = quantidadeQuestoesCorretas * 100 / quantidadeQuestoesRespondidas;

        homeViewModel.Aproveitamento = aproveitamento.ToString() + "%";

        homeViewModel.QuantidadeQuestoesRespondidas = quantidadeQuestoesRespondidas.ToString();


        var quantidadeMateriasRespondidas = 0;

        var materiasRespondidas = new List<string>();

        foreach (var item in listaRegistros)
        {
            materiasRespondidas.Add(item.ListaExercicio.Tipo);
        }

        quantidadeMateriasRespondidas = materiasRespondidas.Distinct().Count();

        homeViewModel.QuantidadeMateriasRespondidas = quantidadeMateriasRespondidas.ToString();

        var listaRegistroOrdenadaAproveitamento = listaRegistros.OrderByDescending(x => x.Acertos * 100 / (x.Acertos + x.Erros)).ToList();

        var dataAgora = DateTime.Now;

        var listaRegistroOrdenadaData = listaRegistros.OrderByDescending(x => x.DataRegistro).ToList();

        homeViewModel.ListaOrdenadaPorData = listaRegistroOrdenadaData;

        var melhorListaRespondida = listaRegistros.OrderByDescending(x => x.Acertos * 100 / (x.Acertos + x.Erros)).ThenByDescending(x => x.DataRegistro).First();

        var melhorListaExercicio = listaExerciciosRespondidos.FirstOrDefault(x => x.ListaExercicioId == melhorListaRespondida.ListaExercicioId);

        var melhorListaRespondidaAproveitamento = melhorListaRespondida.Acertos * 100 / (melhorListaRespondida.Acertos + melhorListaRespondida.Erros);

        homeViewModel.MelhorListaRespondida = melhorListaExercicio;

        homeViewModel.MelhorListaRespondidaAproveitamento = melhorListaRespondidaAproveitamento.ToString() + "%";

        var piorListaRespondida = listaRegistros.OrderBy(x => x.Acertos * 100 / (x.Acertos + x.Erros)).ThenBy(x => x.DataRegistro).First();

        var piorListaRespondidaAproveitamento = piorListaRespondida.Acertos * 100 / (piorListaRespondida.Acertos + piorListaRespondida.Erros);

        var piorListaExercicio = listaExerciciosRespondidos.FirstOrDefault(x => x.ListaExercicioId == piorListaRespondida.ListaExercicioId);

        homeViewModel.PiorListaRespondida = piorListaExercicio;

        homeViewModel.PiorListaRespondidaAproveitamento = piorListaRespondidaAproveitamento.ToString() + "%";

        //agora vamos pegar a melhor e a pior materia

        var listaMaterias = new List<string>();

        foreach (var item in listaExerciciosRespondidos)
        {
            listaMaterias.Add(item.Tipo);
        }

        var listaMateriasDistinct = listaMaterias.Distinct().ToList();

        var listaMateriasRespondidas = new List<string>();

        foreach (var item in listaRegistros)
        {
            listaMateriasRespondidas.Add(item.ListaExercicio.Tipo);
        }

        var listaMateriasRespondidasDistinct = listaMateriasRespondidas.Distinct().ToList();

        var listaMateriasNaoRespondidas = new List<string>();

        foreach (var item in listaMateriasDistinct)
        {
            if (!listaMateriasRespondidasDistinct.Contains(item))
            {
                listaMateriasNaoRespondidas.Add(item);
            }
        }

        var listaMateriasRespondidasAproveitamento = new List<int>();

        foreach (var item in listaMateriasRespondidasDistinct)
        {
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

            listaMateriasRespondidasAproveitamento.Add(aproveitamentoMateria);
        }

        homeViewModel.MelhorMateria = listaMateriasRespondidasDistinct[listaMateriasRespondidasAproveitamento.IndexOf(listaMateriasRespondidasAproveitamento.Max())];

        homeViewModel.PiorMateria = listaMateriasRespondidasDistinct[listaMateriasRespondidasAproveitamento.IndexOf(listaMateriasRespondidasAproveitamento.Min())];

        var quantidadeAcertosMelhorMateria = 0;

        var quantidadeTotalQuestoesMelhorMateria = 0;

        var quantidadeAcertosPiorMateria = 0;

          var quantidadeTotalQuestoesPiorMateria = 0;

        foreach (var item in listaRegistros)
        {
            if (item.ListaExercicio.Tipo == homeViewModel.MelhorMateria)
            {
                quantidadeAcertosMelhorMateria += item.Acertos;
                quantidadeTotalQuestoesMelhorMateria += item.Acertos + item.Erros;

            }
        }

        foreach (var item in listaRegistros)
        {
            if (item.ListaExercicio.Tipo == homeViewModel.PiorMateria)
            {
                quantidadeAcertosPiorMateria += item.Acertos;
                quantidadeTotalQuestoesPiorMateria += item.Acertos + item.Erros;

            }
        }

        var aproveitamentoMelhorMateria = quantidadeAcertosMelhorMateria * 100 / quantidadeTotalQuestoesMelhorMateria;

        var aproveitamentoPiorMateria = quantidadeAcertosPiorMateria * 100 / quantidadeTotalQuestoesPiorMateria;

        homeViewModel.AproveitamentoMelhorMateria = aproveitamentoMelhorMateria.ToString() + "%";

        homeViewModel.AproveitamentoPiorMateria = aproveitamentoPiorMateria.ToString() + "%";

        homeViewModel.QuantidadeQuestoesMelhorMateria = quantidadeTotalQuestoesMelhorMateria.ToString();

        homeViewModel.QuantidadeQuestoesPiorMateria = quantidadeTotalQuestoesPiorMateria.ToString();




        //verificar o role do usuario logado

     


        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        //copiar todas as listas

        var todasListas = _context.ListaExercicios.ToList();

        //copiar todos os exercicios

        var todosExercicios = _context.Exercicios.ToList();

        //copiar todas as alternativas

        var todasAlternativas = _context.Alternativas.ToList();

        //usar o include para pegar nas listas os exercicios e as alternativas

        var listaExercicios = _context.ListaExercicios.Include(x => x.Exercicios).ThenInclude(x => x.Alternativas).ToList();

      


        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



}
