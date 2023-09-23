using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class Alternativa
    {
        public int AlternativaId { get; set; }


     
        public string Texto { get; set; }

     

        public int ExercicioId { get; set; }

        
    }
}

