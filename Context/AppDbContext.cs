using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResolverQuestao.Models;

namespace ResolverQuestao.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }

        public DbSet<ListaExercicio> ListaExercicios { get; set; }
        
    }
}