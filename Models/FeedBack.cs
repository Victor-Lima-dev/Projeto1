using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class FeedBack
    {
        public int FeedBackId { get; set; }

        public string Descricao { get; set; }

        public int ExercicioId { get; set; }

        public Exercicio Exercicio { get; set; }

        public Boolean Avaliada { get; set; } = false;

        public string? Avaliacao { get; set; }

        public Boolean Resolvida { get; set; } = false;

        public string? Solucao { get; set; }

    }
}