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
        /// Cadastra Dias
        /// </summary>
        /// <param name="dia">Parametro dia</param>
        /// <remarks>
        /// Modelo de Dados deve ser enviado para cadastrar dia request:
        /// 
        /// POST /Dias
        /// {
        ///     "dia":"nome do dia"
        /// }
        /// </remarks>
        /// <response code="200">Retorna dia cadastrado</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(Dias), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public IActionResult Cadastrar([FromBody] Dias dia)
        {
             try
            {
                contexto.Dias.Add(dia);
                contexto.SaveChanges();
                return Ok(dia);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o dia pelo parametro id
        /// </summary>
        /// <remarks>
        /// Modelo de dia que irá ser atualizada request:
        ///    PUT /Dias
        ///    {
        ///       "idDias":0,
        ///       "Dia":"Nome do dia atualizado"
        ///    }   
        /// </remarks>
        /// <param name="id">parametro id do dia</param>
        /// <param name="dia">Dia</param>
        /// <returns>Retorna o dia atualizada</returns>
        /// <response code="200">Retorna dia atualizado</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Dia não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Dias), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
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