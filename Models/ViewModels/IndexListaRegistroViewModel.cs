using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Models.ViewModels
{
    public class IndexListaRegistroViewModel
    {
        public List<ListaRegistro> ListaRegistros { get; set; } = new List<ListaRegistro>();

        public List<string> Materias = new();

        public List<string> Tipos = new();


    }
}