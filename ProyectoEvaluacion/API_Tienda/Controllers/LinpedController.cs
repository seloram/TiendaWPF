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
    public class LinpedController : ControllerBase
    {
        private readonly TodoContext _context;

        public LinpedController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Linped
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Linped>>> GetLinped()
        {
            return await _context.Linped.ToListAsync();
        }

        // Modificación necesaria por tener PK compuesta
        // GET: api/Linped/5:4
        [HttpGet("{id_pedido}:{id_linea}")]
        public async Task<ActionResult<Linped>> GetLinped(int id_pedido,int id_linea)
        {

            string aux = System.Convert.ToString(id_pedido);
            Object[] param = new Object[] { aux,id_linea };

            var linped = await _context.Linped.FindAsync(param);

            if (linped == null)
            { 
               return NotFound();
            }
            return linped;
        }

        // PUT: api/Linped/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinped(int id, Linped linped)
        {
            if (id != linped.linea)
            {
                return BadRequest();
            }

            _context.Entry(linped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinpedExists(id))
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

        // POST: api/Linped
        [HttpPost]
        public async Task<ActionResult<Linped>> PostLinped(Linped linped)
        {
            _context.Linped.Add(linped);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLinped", new { id = linped.linea }, linped);
        }

        // Modificación necesaria por tener PK compuesta
        // DELETE: api/Linped/5:4
        [HttpDelete("{id_pedido}:{id_linea}")]
        public async Task<ActionResult<Linped>> DeleteLinped(int id_pedido,int id_linea)
        {
            string aux = System.Convert.ToString(id_pedido);
            Object[] param = new Object[] { aux, id_linea };

            var linped = await _context.Linped.FindAsync(param);
         
            if (linped == null)
            {
                return NotFound();
            }

            _context.Linped.Remove(linped);
            await _context.SaveChangesAsync();

            return linped;
        }

        private bool LinpedExists(int id)
        {
            return _context.Linped.Any(e => e.linea == id);
        }
    }
}
