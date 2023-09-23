using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ResolverQuestao.Context;
using ResolverQuestao.Models;

namespace ResolverQuestao.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class GPTController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GPTController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var listaExercicios = _context.ListaExercicios.Include(le => le.Exercicios).ToList();
            var listaJson = JsonConvert.SerializeObject(listaExercicios, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(listaJson);
        }


        [HttpPost("ListaExercicios")]
        public async Task<IActionResult> ListaExercicios(List<ListaExercicio> listas)
        {


            //adicionar as listas de exercicios no banco de dados

            foreach (var item in listas)
            {
                _context.ListaExercicios.Add(item);
            }

            //salvar as alteraçoes no banco de dados

            await _context.SaveChangesAsync();
     
            return Ok();
        }





        [HttpGet("EnviarJSON")]
        public async Task<IActionResult> EnviarJSON(string json)
        {

            var exercicio = Newtonsoft.Json.JsonConvert.DeserializeObject<Exercicio>(json);

            _context.Exercicios.Add(exercicio);

            return Ok();

        }

    }
}