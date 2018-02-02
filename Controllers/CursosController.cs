using System.Collections.Generic;
using System.Linq;
using CursosLivre.Dados;
using CursosLivre.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursosLivre.Controllers
{
    [Route("api/[controller]")]

    public class CursosController : Controller
    {
        Cursos curso = new Cursos();

        readonly CursosLivreContexto contexto;

        public CursosController(CursosLivreContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Cursos> Listar()
        {
            return contexto.Cursos.ToList();
        }

        [HttpGet("{id}")]
        public Cursos Listar(int id)
        {
            return contexto.Cursos.Where(x => x.IdCursos == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Cursos curso)
        {
            contexto.Cursos.Add(curso);
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Cursos curso)
        {
            if (curso == null || curso.IdCursos != id)
            {
                return BadRequest();
            }

            var cursinho = contexto.Cursos.FirstOrDefault(x => x.IdCursos == id);
            if (cursinho == null)
            {
                return NotFound();
            }

            cursinho.IdAreas = curso.IdAreas;
            cursinho.NomeCurso = curso.NomeCurso;
                    
            contexto.Cursos.Update(cursinho);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }


    }
}