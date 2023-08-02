using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class ListaExercicioHomeViewModel
    {
        public List<ListaExercicio> ListaExercicios { get; set; } = new List<ListaExercicio>();
        public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();
    }
}