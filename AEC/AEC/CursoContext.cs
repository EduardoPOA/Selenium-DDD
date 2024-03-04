using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC
{
    //criando uma herança do DbContext para representar o bando de dados
    public class CursoContext : DbContext
    {
        public DbSet<CursoDTO> Cursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //aqui estou declarando nome da virtual table
            optionsBuilder.UseInMemoryDatabase("AEC");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelando o ID para poder inserir o restante do DTO
            modelBuilder.Entity<CursoDTO>().HasKey(c => c.CursoId);
        }
    }
}
