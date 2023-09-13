using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class UsuarioBancoDados
    {
        public int UsuarioBancoDadosId { get; set; }

        public Guid UsuarioGuidId { get; set; }

        public string Nome { get; set; } 

        public string Email { get; set; } = "sem email";

        public string Profissao { get; set; }

        public string Objetivo { get; set; }

         public string MediaGeral { get; set; } = "nulo";

         public string ExerciciosResolvidos { get; set; } = "nulo";


    }
}