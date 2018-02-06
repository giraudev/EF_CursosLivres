using System.Collections.Generic;
using System.Linq;
using CursosLivre.Dados;
using CursosLivre.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursosLivre.Controllers
{
    [Route("api/[controller]")]
    public class DiasController : Controller
    {
        Dias dias = new Dias();

        readonly CursosLivreContexto contexto;

        public DiasController(CursosLivreContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Retorna lista de dias
        /// </summary>
        /// <returns>lista de dias</returns>
        /// <response code="200">Retorna uma lista de dias</response>
        ///<response code="400">Ocorreu um erro</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Dias>), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public IActionResult Listar()
        {
            try
            {
                return Ok(contexto.Dias.ToList());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Retorna dia, atraves de seu id
        /// </summary>
        /// <param name="id">id dias</param>
        /// <returns>dia</returns>
        /// <response code="200">Retorna um dia</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Dia não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dias), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 400)]
      
        public IActionResult Listar(int id)
        {
            try
            {
                Dias dia_ = contexto.Dias.FirstOrDefault(x => x.IdDias == id);

                if (dia_ == null)
                {
                    return NotFound("Dia não encontrado");
                }
                return Ok(dia_);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra novo dia
        /// </summary>
        /// <param name="dia">dia</param>
        [HttpPost]
        public void Cadastrar([FromBody] Dias dia)
        {
            contexto.Dias.Add(dia);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Atualiza dia, atraves de seu id
        /// </summary>
        /// <param name="id">id dia</param>
        /// <param name="dia">dia</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Dias dia)
        {
            if (dia == null || dia.IdDias != id)
            {
                return BadRequest();
            }
            var dia1 = contexto.Dias.FirstOrDefault(x => x.IdDias == id);
            if (dia1 == null)
            {
                return NotFound();
            }

            dia1.Dia = dia.Dia;


            contexto.Dias.Update(dia1);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Deleta dia atraves de seu id
        /// </summary>
        /// <param name="id">id dias</param>
        /// <returns>msg json</returns>
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {
            var dia = contexto.Dias.Where(x => x.IdDias == id).FirstOrDefault();
            if (dia == null)
            {
                return NotFound();
            }

            contexto.Dias.Remove(dia);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }

    }
}