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

        [HttpGet]
        public IEnumerable<Dias> Listar()
        {
            return contexto.Dias.ToList();
        }

        [HttpGet("{id}")]
        public Dias Listar(int id)
        {
            return contexto.Dias.Where(x => x.IdDias == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Dias dia)
        {
            contexto.Dias.Add(dia);
            contexto.SaveChanges();
        }

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