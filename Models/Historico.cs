using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class Historico
    {
        public int HistoricoId { get; set; }
        public List<DateTime> Data { get; set; } = new List<DateTime>();
        public string Sequencia { get; set; }

        public void AvaliarSequencia(List<DateTime> data)
        {
            var contagemDatas = data.Count();
            var sequencia = contagemDatas.ToString();

            //pegar os dias da sequencia em relação ao atual
            var dias = data.Select(x => (DateTime.Now - x).Days).ToList();
        }


    }
}