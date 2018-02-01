using CursosLivre.Models;
using Microsoft.EntityFrameworkCore;

namespace CursosLivre.Dados

{
    public class CursosLivreContexto:DbContext
    {
        public CursosLivreContexto(DbContextOptions<CursosLivreContexto> options):base(options){}

        public DbSet<Areas> Areas {get;set;}
        public DbSet<Cronogramas> Cronogramas {get; set;}
        public DbSet<Cursos> Cursos {get; set;}
        public DbSet<Dias> Dias {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Areas>().ToTable("Areas");
            modelBuilder.Entity<Cronogramas>().ToTable("Cronogramas");
            modelBuilder.Entity<Cursos>().ToTable("Cursos");
            modelBuilder.Entity<Dias>().ToTable("Dias");}

        
    }
}
