using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vitrine_backend.Entidades;

namespace vitrine_backend.Infraestrutura
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<Oferta> Oferta { get; set; } 
    }
}
