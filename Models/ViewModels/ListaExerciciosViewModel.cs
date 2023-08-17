using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResolverQuestao.Models;

namespace ResolverQuestao.Models.ViewModels
{
    public class ListaExerciciosViewModel
    {
        public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();

        public ListaExercicio ListaExercicio { get; set; } = new ListaExercicio();
        public List<int> ExerciciosSelecionados { get; set; } = new List<int>();
        public List<int> ExerciciosSelecionadosEdit { get; set; } = new List<int>();


        public List<Exercicio> TodosExercicios { get; set; } = new List<Exercicio>();

        public List<Exercicio> ExerciciosSemLista { get; set; } = new List<Exercicio>();

        public List<TopicoLista> TopicoListas { get; set; } = new List<TopicoLista>();

        


    }
}