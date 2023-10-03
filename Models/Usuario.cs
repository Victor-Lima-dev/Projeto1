using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models
{
    public class Usuario
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string UsuarioId { get; set; }
        public string ReturnUrl { get; set; }
    }
}