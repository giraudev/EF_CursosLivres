using System.Collections.Generic;
using System.Linq;
using CursosLivre.Dados;
using CursosLivre.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursosLivre.Controllers
{
    /// <summary>
    /// Classe controller de Area
    /// </summary>
    [Route("api/[controller]")]

    public class AreasController : Controller
    {
        Areas areas = new Areas();
        readonly CursosLivreContexto contexto;


        public AreasController(CursosLivreContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Retorna lista de Areas
        /// </summary>
        /// <returns>Lista de Areas</returns>
        [HttpGet]
        public IEnumerable<Areas> Listar()
        {
            return contexto.Areas.ToList();
        }

        /// <summary>
        /// Retorna uma Area, pelo parametro "id"
        /// </summary>
        /// <param name="id">parametro id</param>
        /// <returns>Area</returns>
        [HttpGet("{id}")]
        public Areas Listar(int id)
        {
            return contexto.Areas.Where(x => x.IdArea == id).FirstOrDefault();
        }

        /// <summary>
        /// Cadastra Area
        /// </summary>
        /// <param name="area">Parametro area</param>
        [HttpPost]
        public void Cadastrar([FromBody] Areas area)
        {
            contexto.Areas.Add(area);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Atualiza a Area pelo parametro id
        /// </summary>
        /// <param name="id">parametro id da Area</param>
        /// <param name="area">Area</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Areas area)
        {
            if (area == null || area.IdArea != id)
            {
                return BadRequest();
            }

            var area1 = contexto.Areas.FirstOrDefault(x => x.IdArea == id);
            if (area1 == null)
            {
                return NotFound();
            }

            area1.NomeArea = area.NomeArea;


            contexto.Areas.Update(area1);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Deleta Area pelo parametro id
        /// </summary>
        /// <param name="id">id da Area</param>
        /// <returns>retorna json com msg</returns>
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {
            var area = contexto.Areas.Where(x => x.IdArea == id).FirstOrDefault();
            if (area == null)
            {
                return NotFound();
            }

            contexto.Areas.Remove(area);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}