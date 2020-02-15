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
    public class MemoriasController : ControllerBase
    {
        private readonly TodoContext _context;

        public MemoriasController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Memorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Memoria>>> GetMemoria()
        {
            return await _context.Memoria.ToListAsync();
        }

        // GET: api/Memorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memoria>> GetMemoria(string id)
        {
            var memoria = await _context.Memoria.FindAsync(id);

            if (memoria == null)
            {
                return NotFound();
            }

            return memoria;
        }

        // PUT: api/Memorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemoria(string id, Memoria memoria)
        {
            if (id != memoria.memoriaID)
            {
                return BadRequest();
            }

            _context.Entry(memoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoriaExists(id))
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

        // POST: api/Memorias
        [HttpPost]
        public async Task<ActionResult<Memoria>> PostMemoria(Memoria memoria)
        {
            _context.Memoria.Add(memoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemoria", new { id = memoria.memoriaID }, memoria);
        }

        // DELETE: api/Memorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Memoria>> DeleteMemoria(string id)
        {
            var memoria = await _context.Memoria.FindAsync(id);
            if (memoria == null)
            {
                return NotFound();
            }

            _context.Memoria.Remove(memoria);
            await _context.SaveChangesAsync();

            return memoria;
        }

        private bool MemoriaExists(string id)
        {
            return _context.Memoria.Any(e => e.memoriaID == id);
        }
    }
}
