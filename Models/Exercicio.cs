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

        [StringLength(100, MinimumLength = 3, ErrorMessage = "O enunciado deve ter entre 3 e 100 caracteres.")]
        public string Enunciado { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "O tipo deve começar com uma letra maiúscula e conter apenas letras e espaços.")]
        public string Tipo { get; set; }


        public string Resposta { get; set; }

        [MinLength(2, ErrorMessage = "O exercício deve ter pelo menos duas alternativas.")]
        public List<Alternativa> Alternativas { get; set; } = new List<Alternativa>();


        public string? Explicacao { get; set; }

        public string? MaterialSuporte { get; set; }


        //relaçao muito para muitos, ListaExercicios

        public List<ListaExercicio> ListaExercicios { get; set; } = new List<ListaExercicio>();

        public string UsuarioId { get; set; }

          


    }
}