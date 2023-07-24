using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class Exercicio
    {
        public int ExercicioId { get; set; }

        public string Enunciado { get; set; }

        public string Titulo { get; set; }

        public string Tipo { get; set; }

        public string Resposta { get; set; }

        public List<Alternativa> Alternativas { get; set; }
    }
}