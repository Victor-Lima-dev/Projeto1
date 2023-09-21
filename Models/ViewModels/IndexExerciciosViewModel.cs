using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class IndexExerciciosViewModel
    {
        public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();

        public List<string> Tipos { get; set; } = new List<string>();
    }
}