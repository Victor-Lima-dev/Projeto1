using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResolverQuestao.Models;

namespace ResolverQuestao.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }

        public DbSet<ListaExercicio> ListaExercicios { get; set; }

        public DbSet<ListaRegistro> ListaRegistros { get; set; }
        
        public DbSet<TopicoLista> TopicoListas { get; set; }
    }
}