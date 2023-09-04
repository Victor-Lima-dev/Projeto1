using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class ListaRegistro
    {
        public int ListaRegistroId { get; set; }

        public int ListaExercicioId { get; set; }

        public string UsuarioId { get; set; }

        public DateTime DataRegistro { get; set; }

        public int Acertos { get; set; }

        public int Erros { get; set; }

        public string TituloLista { get; set; }

        public ListaExercicio ListaExercicio { get; set; }

    }
}