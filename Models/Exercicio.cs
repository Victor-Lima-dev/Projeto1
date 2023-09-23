using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        
        public List<Alternativa> Alternativas { get; set; } = new List<Alternativa>();


        public string? Explicacao { get; set; }

        public string? MaterialSuporte { get; set; }


        //relaçao muito para muitos, ListaExercicios

        public List<ListaExercicio> ListaExercicios { get; set; } = new List<ListaExercicio>();

        public string UsuarioId { get; set; }

        public string? Materia { get; set; }


          


    }
}