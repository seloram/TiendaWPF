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
    public class TvsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TvsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Tvs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tv>>> GetTv()
        {
            return await _context.Tv.ToListAsync();
        }

        // GET: api/Tvs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tv>> GetTv(string id)
        {
            var tv = await _context.Tv.FindAsync(id);

            if (tv == null)
            {
                return NotFound();
            }

            return tv;
        }

        // PUT: api/Tvs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTv(string id, Tv tv)
        {
            if (id != tv.tvID)
            {
                return BadRequest();
            }

            _context.Entry(tv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TvExists(id))
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

        // POST: api/Tvs
        [HttpPost]
        public async Task<ActionResult<Tv>> PostTv(Tv tv)
        {
            _context.Tv.Add(tv);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTv", new { id = tv.tvID }, tv);
        }

        // DELETE: api/Tvs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tv>> DeleteTv(string id)
        {
            var tv = await _context.Tv.FindAsync(id);
            if (tv == null)
            {
                return NotFound();
            }

            _context.Tv.Remove(tv);
            await _context.SaveChangesAsync();

            return tv;
        }

        private bool TvExists(string id)
        {
            return _context.Tv.Any(e => e.tvID == id);
        }
    }
}
