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
    public class tipoArticulosController : ControllerBase
    {
        private readonly TodoContext _context;

        public tipoArticulosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/tipoArticulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tipoArticulo>>> GettipoArticulo()
        {
            return await _context.tipoArticulo.ToListAsync();
        }

        // GET: api/tipoArticulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tipoArticulo>> GettipoArticulo(int id)
        {
            var tipoArticulo = await _context.tipoArticulo.FindAsync(id);

            if (tipoArticulo == null)
            {
                return NotFound();
            }

            return tipoArticulo;
        }

        // PUT: api/tipoArticulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttipoArticulo(int id, tipoArticulo tipoArticulo)
        {
            if (id != tipoArticulo.tipoArticuloID)
            {
                return BadRequest();
            }

            _context.Entry(tipoArticulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoArticuloExists(id))
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

        // POST: api/tipoArticulos
        [HttpPost]
        public async Task<ActionResult<tipoArticulo>> PosttipoArticulo(tipoArticulo tipoArticulo)
        {
            _context.tipoArticulo.Add(tipoArticulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettipoArticulo", new { id = tipoArticulo.tipoArticuloID }, tipoArticulo);
        }

        // DELETE: api/tipoArticulos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<tipoArticulo>> DeletetipoArticulo(int id)
        {
            var tipoArticulo = await _context.tipoArticulo.FindAsync(id);
            if (tipoArticulo == null)
            {
                return NotFound();
            }

            _context.tipoArticulo.Remove(tipoArticulo);
            await _context.SaveChangesAsync();

            return tipoArticulo;
        }

        private bool tipoArticuloExists(int id)
        {
            return _context.tipoArticulo.Any(e => e.tipoArticuloID == id);
        }
    }
}
