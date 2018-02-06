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
        /// <response code="200">Retorna uma lista de areas</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Areas>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(contexto.Areas.ToList());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma Area, pelo parametro "id"
        /// </summary>
        /// <param name="id">parametro id</param>
        /// <returns>Area</returns>
        /// <response code="200">Retorna uma area</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Área não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Areas), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Listar(int id)
        {
            try
            {
                Areas area_ = contexto.Areas.FirstOrDefault(x => x.IdArea == id);

                if (area_ == null)
                {
                    return NotFound("Área não encontrada");
                }
                return Ok(area_);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Cadastra Area
        /// </summary>
        /// <param name="area">Parametro area</param>
        /// <remarks>
        /// Modelo de Dados deve ser enviado para cadastrar a area request:
        /// 
        /// POST /Area
        /// {
        ///     "nomeArea":"nome da área"
        /// }
        /// </remarks>
        /// <response code="200">Retorna a area cadastrada</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(Areas), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Cadastrar([FromBody] Areas area)
        {
            try
            {
                contexto.Areas.Add(area);
                contexto.SaveChanges();
                return Ok(area);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a Area pelo parametro id
        /// </summary>
        /// <remarks>
        /// Modelo de área que irá ser atualizada request:
        ///    PUT /Area
        ///    {
        ///       "idArea":0,
        ///       "nomeArea":"Nome de area atualizado"
        ///    }   
        /// </remarks>
        /// <param name="id">parametro id da Area</param>
        /// <param name="area">Area</param>
        /// <returns>Retorna a area atualizada</returns>
        /// <response code="200">Retorna area atualizada</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Área não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Areas), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
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