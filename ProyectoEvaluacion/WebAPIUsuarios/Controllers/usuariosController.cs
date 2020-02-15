using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIUsuarios.Models;

namespace WebAPIUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly dbContext _context;

        public usuariosController(dbContext context)
        {
            _context = context;
        }

        // Obtencion de todos los usuarios
        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Obtencion de un usuario por id
        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<usuarios>> Getusuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // Modificación de un usuario
        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusuarios(int id, usuarios usuarios)
        {
            if (id != usuarios.usuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuariosExists(id))
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

        // Inserción de un nuevo usuario
        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<usuarios>> Postusuarios(usuarios usuarios)
        {
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuarios", new { id = usuarios.usuarioId }, usuarios);
        }

        // Eliminación de un usuario
        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<usuarios>> Deleteusuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return usuarios;
        }

        private bool usuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.usuarioId == id);
        }
    }
}
