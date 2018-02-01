using System;
using System.Linq;
using CursosLivre.Models;

namespace CursosLivre.Dados
{
    public class IniciarBanco
    {
        public static void Inicializar(CursosLivreContexto contexto)
        {
            contexto.Database.EnsureCreated();

            if (contexto.Areas.Any())
            {
                return;
            }

            var area = new Areas(){
                NomeArea = "BELEZA E ESTÉTICA"
            };
            contexto.Areas.Add(area);

            var curso = new Cursos(){
                IdAreas=area.IdArea, NomeCurso="Depilação", 
            };
            contexto.Cursos.Add(curso);

            var cronograma = new Cronogramas(){
                IdCursos=curso.IdCursos, HoraInicio = DateTime.Parse("08:30"), HoraFim = DateTime.Parse("16:30"), DiaInicio=DateTime.Parse("24/03/2018"), 
                DiaFim=DateTime.Parse("05/05/2018")
            };
            contexto.Cronogramas.Add(cronograma);
            
            var dia = new Dias(){
                Dia = "Sábado", IdCronograma=cronograma.IdCronograma
            };
            contexto.Dias.Add(dia);

            contexto.SaveChanges();

        }
    }
}