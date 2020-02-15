using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Tienda.Models;

namespace API_Tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivosController : ControllerBase
    {
        private readonly TodoContext _context;

        public ObjetivosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Objetivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objetivo>>> GetObjetivo()
        {
            return await _context.Objetivo.ToListAsync();
        }

        // GET: api/Objetivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Objetivo>> GetObjetivo(string id)
        {
            var objetivo = await _context.Objetivo.FindAsync(id);

            if (objetivo == null)
            {
                return NotFound();
            }

            return objetivo;
        }

        // PUT: api/Objetivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObjetivo(string id, Objetivo objetivo)
        {
            if (id != objetivo.objetivoID)
            {
                return BadRequest();
            }

            _context.Entry(objetivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjetivoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Objetivos
        [HttpPost]
        public async Task<ActionResult<Objetivo>> PostObjetivo(Objetivo objetivo)
        {
            _context.Objetivo.Add(objetivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObjetivo", new { id = objetivo.objetivoID }, objetivo);
        }

        // DELETE: api/Objetivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Objetivo>> DeleteObjetivo(string id)
        {
            var objetivo = await _context.Objetivo.FindAsync(id);
            if (objetivo == null)
            {
                return NotFound();
            }

            _context.Objetivo.Remove(objetivo);
            await _context.SaveChangesAsync();

            return objetivo;
        }

        private bool ObjetivoExists(string id)
        {
            return _context.Objetivo.Any(e => e.objetivoID == id);
        }
    }
}
