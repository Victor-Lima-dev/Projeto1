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

    }
}