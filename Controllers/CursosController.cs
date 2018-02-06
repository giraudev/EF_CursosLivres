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

        /// <summary>
        /// Retorna lista de cursos
        /// </summary>
        /// <returns>lista de cursos</returns>
        [HttpGet]
        public IEnumerable<Cursos> Listar()
        {
            return contexto.Cursos.ToList();
        }

        /// <summary>
        /// Retorna curso atraves de seu id
        /// </summary>
        /// <param name="id">id curso</param>
        /// <returns>curso</returns>
        [HttpGet("{id}")]
        public Cursos Listar(int id)
        {
            return contexto.Cursos.Where(x => x.IdCursos == id).FirstOrDefault();
        }

        /// <summary>
        /// Cadastra novo curso
        /// </summary>
        /// <param name="curso">curso</param>
        [HttpPost]
        public void Cadastrar([FromBody] Cursos curso)
        {
            contexto.Cursos.Add(curso);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Atualiza curso atraves de deu id
        /// </summary>
        /// <param name="id">id curso</param>
        /// <param name="curso">curso</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deleta curso atraves de seu id
        /// </summary>
        /// <param name="id">id curso</param>
        /// <returns>msg json</returns>
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {
            var curso = contexto.Cursos.Where(x => x.IdCursos == id).FirstOrDefault();
            if (curso == null)
            {
                return NotFound();
            }

            contexto.Cursos.Remove(curso);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }


    }
}