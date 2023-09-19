using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet("EnviarJSON")]
        public async Task<IActionResult> EnviarJSON(string json)
        {
            




            var exercicio = Newtonsoft.Json.JsonConvert.DeserializeObject<Exercicio>(json);

            _context.Exercicios.Add(exercicio);
           
            return Ok();

        }

    }
}