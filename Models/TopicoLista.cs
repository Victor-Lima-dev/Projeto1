using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class TopicoLista
    {
        public int TopicoListaId { get; set; }

        public int ListaExercicioId { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }
    }
}