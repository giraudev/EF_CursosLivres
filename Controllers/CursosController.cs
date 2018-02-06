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
        /// <response code="200">Retorna uma lista de cursos</response>
        ///<response code="400">Ocorreu um erro</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Cursos>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(contexto.Cursos.ToList());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna curso atraves de seu id
        /// </summary>
        /// <param name="id">id curso</param>
        /// <returns>curso</returns>
        /// <response code="200">Retorna um curso</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Curso não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cursos), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar(int id)
        {
            try
            {
                Cursos cursos_ = contexto.Cursos.FirstOrDefault(x => x.IdCursos == id);

                if (cursos_ == null)
                {
                    return NotFound("Dia não encontrado");
                }
                return Ok(cursos_);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
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