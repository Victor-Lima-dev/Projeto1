using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class ListaExercicio
    {
        public int ListaExercicioId { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public string? MaterialSuporte { get; set; }


        public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();

        public string UsuarioId { get; set; }


        public int IndiceExercicio { get; set; } = 0;

        


    }
}