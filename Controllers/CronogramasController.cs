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

        [HttpGet]
        public IEnumerable<Cronogramas> Listar()
        {
            return contexto.Cronogramas.ToList();
        }

        [HttpGet("{id}")]
        public Cronogramas Listar(int id)
        {
            return contexto.Cronogramas.Where(x => x.IdCronograma == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Cronogramas cronograma)
        {
            contexto.Cronogramas.Add(cronograma);
            contexto.SaveChanges();
        }

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



    }
}