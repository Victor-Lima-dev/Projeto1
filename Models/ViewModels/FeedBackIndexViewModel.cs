using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class FeedBackIndexViewModel
    {
        public List<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

        public List<FeedBack> FeedBacksSelecionados { get; set; } = new List<FeedBack>();

        public Boolean Filtro { get; set; } = false;

        public List<string> Tipos { get; set; } = new List<string>();
    }
}