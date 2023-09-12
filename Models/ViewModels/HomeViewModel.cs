using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class HomeViewModel
    {
        public string Aproveitamento { get; set; } = "0";

        public string QuantidadeQuestoesRespondidas { get; set; } = "0";

        public string QuantidadeMateriasRespondidas { get; set; } = "0";

        public ListaExercicio MelhorListaRespondida { get; set; } = new ListaExercicio();

        public ListaExercicio PiorListaRespondida { get; set; } = new ListaExercicio();

        public string MelhorListaRespondidaAproveitamento { get; set; } = "0";

        public string PiorListaRespondidaAproveitamento { get; set; } = "0";

        public List<ListaRegistro> ListaOrdenadaPorData { get; set; } = new List<ListaRegistro>();

        public string PiorMateria { get; set; } = "";

        public string MelhorMateria { get; set; } = "";

        public string AproveitamentoMelhorMateria { get; set; } = "";

        public string QuantidadeQuestoesMelhorMateria { get; set; } = "";

        public string AproveitamentoPiorMateria { get; set; } = "";

        public string QuantidadeQuestoesPiorMateria { get; set; } = "";

      
    }
}