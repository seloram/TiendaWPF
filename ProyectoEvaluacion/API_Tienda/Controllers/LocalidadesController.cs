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
    public class LocalidadesController : ControllerBase
    {
        private readonly TodoContext _context;

        public LocalidadesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Localidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localidad>>> GetLocalidad()
        {
            return await _context.Localidad.ToListAsync();
        }

        // GET: api/Localidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Localidad>> GetLocalidad(string id)
        {
            var localidad = await _context.Localidad.FindAsync(id);

            if (localidad == null)
            {
                return NotFound();
            }

            return localidad;
        }

        // PUT: api/Localidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalidad(string id, Localidad localidad)
        {
            if (id != localidad.provinciaID)
            {
                return BadRequest();
            }

            _context.Entry(localidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalidadExists(id))
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

        // POST: api/Localidades
        [HttpPost]
        public async Task<ActionResult<Localidad>> PostLocalidad(Localidad localidad)
        {
            _context.Localidad.Add(localidad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocalidadExists(localidad.provinciaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLocalidad", new { id = localidad.provinciaID }, localidad);
        }

        // DELETE: api/Localidades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localidad>> DeleteLocalidad(string id)
        {
            var localidad = await _context.Localidad.FindAsync(id);
            if (localidad == null)
            {
                return NotFound();
            }

            _context.Localidad.Remove(localidad);
            await _context.SaveChangesAsync();

            return localidad;
        }

        private bool LocalidadExists(string id)
        {
            return _context.Localidad.Any(e => e.provinciaID == id);
        }
    }
}
