using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class HistoricoMateria
    {
        public string NomeMateria { get; set; }

        public int QuantidadeQuestoesCorretas { get; set; }

        public int QuantidadeQuestoesErradas { get; set; }
        public int QuantidadeQuestoesRespondidas { get; set; }

        public string PorcentagemAcerto { get; set; }
        

        [NotMapped]
        public DateTime UltimaDataResposta { get; set; }

    }
}