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
    public class CamarasController : ControllerBase
    {
        private readonly TodoContext _context;

        public CamarasController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Camaras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camara>>> GetCamara()
        {
            return await _context.Camara.ToListAsync();
        }

        // GET: api/Camaras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camara>> GetCamara(string id)
        {
            var camara = await _context.Camara.FindAsync(id);

            if (camara == null)
            {
                return NotFound();
            }

            return camara;
        }

        // PUT: api/Camaras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamara(string id, Camara camara)
        {
            if (id != camara.camaraID)
            {
                return BadRequest();
            }

            _context.Entry(camara).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamaraExists(id))
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

        // POST: api/Camaras
        [HttpPost]
        public async Task<ActionResult<Camara>> PostCamara(Camara camara)
        {
            _context.Camara.Add(camara);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamara", new { id = camara.camaraID }, camara);
        }

        // DELETE: api/Camaras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Camara>> DeleteCamara(string id)
        {
            var camara = await _context.Camara.FindAsync(id);
            if (camara == null)
            {
                return NotFound();
            }

            _context.Camara.Remove(camara);
            await _context.SaveChangesAsync();

            return camara;
        }

        private bool CamaraExists(string id)
        {
            return _context.Camara.Any(e => e.camaraID == id);
        }
    }
}
