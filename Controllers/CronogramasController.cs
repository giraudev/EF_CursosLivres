using System.Collections.Generic;
using System.Linq;
using CursosLivre.Dados;
using CursosLivre.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursosLivre.Controllers
{
    [Route("api/[controller]")]
    public class CronogramasController : Controller
    {

        Cronogramas cronogramas = new Cronogramas();
        readonly CursosLivreContexto contexto;

        public CronogramasController(CursosLivreContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Retorna lista de Cronogramas
        /// </summary>
        /// <returns>Cronogramas</returns>
        /// <response code="200">Retorna uma lista de cronogramas</response>
        ///<response code="400">Ocorreu um erro</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Cronogramas>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(contexto.Cronogramas.ToList());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        /* Código antigo para HttpGet:
        public IEnumerable<Cronogramas> Listar()
         {
             return contexto.Cronogramas.ToList();
         }*/

        /// <summary>
        /// Retorna um Cronograma, pelo parametro id
        /// </summary>
        /// <param name="id">id de cronograma</param>
        /// <returns>cronograma</returns>
        /// <response code="200">Retorna um cronograma</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Cronograma não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cronogramas), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar(int id)
        {
            try
            {
                Cronogramas cronogramas_ = contexto.Cronogramas.FirstOrDefault(x => x.IdCronograma == id);

                if (cronogramas_ == null)
                {
                    return NotFound("Dia não encontrado");
                }
                return Ok(cronogramas_);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /* Antigo código para HttpGet com parametro id:
        public Cronogramas Listar(int id)
        {
            return contexto.Cronogramas.Where(x => x.IdCronograma == id).FirstOrDefault();
        }*/

        /// <summary>
        /// Cadastra um cronograma novo
        /// </summary>
        /// <param name="cronograma">cronograma</param>
        [HttpPost]
        public void Cadastrar([FromBody] Cronogramas cronograma)
        {
            contexto.Cronogramas.Add(cronograma);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Atualiza um cronograma existente
        /// </summary>
        /// <param name="id">id cronograma</param>
        /// <param name="cronograma">cronograma</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Cronogramas cronograma)
        {
            if (cronograma == null || cronograma.IdCronograma != id)
            {
                return BadRequest();
            }

            var cron = contexto.Cronogramas.FirstOrDefault(x => x.IdCronograma == id);
            if (cron == null)
            {
                return NotFound();
            }

            cron.IdCursos = cronograma.IdCursos;
            cron.HoraInicio = cronograma.HoraInicio;
            cron.HoraFim = cronograma.HoraFim;
            cron.DiaInicio = cronograma.DiaInicio;
            cron.DiaFim = cronograma.DiaFim;

            contexto.Cronogramas.Update(cron);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Deleta um cronograma atraves de deu id
        /// </summary>
        /// <param name="id">id cronograma</param>
        /// <returns>msg json</returns>
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {
            var cronograma = contexto.Cronogramas.Where(x => x.IdCronograma == id).FirstOrDefault();
            if (cronograma == null)
            {
                return NotFound();
            }

            contexto.Cronogramas.Remove(cronograma);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }



    }
}