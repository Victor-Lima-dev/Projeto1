using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class IndexListaExercicioViewModel
    {
        public List<ListaExercicio> ListaExercicios { get; set; } = new List<ListaExercicio>();
        public List<ListaExercicio> ListasSelecionadas { get; set; } = new List<ListaExercicio>();

        public Boolean Filtro { get; set; } = false;

        public List<string> Tipos { get; set; } = new List<string>();
    }
}