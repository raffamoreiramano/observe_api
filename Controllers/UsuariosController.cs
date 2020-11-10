using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Observe.Data;
using Observe.Models;

namespace Observe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/cid/xyz5
        [HttpGet("cid/{cid}")]
        public async Task<ActionResult<Usuario>> GetUsuarioByCid(string cid)
        {
            var usuario = await _context.Usuarios.Where(u => u.CID == cid).SingleOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/nome/Usuario
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioByNome(string nome)
        {
            return await _context.Usuarios
                .Where(u => 
                    EF.Functions.Like(u.Nome, String.Format("%{0}%", nome)) 
                    || 
                    EF.Functions.Like(u.Sobrenome, String.Format("%{0}%", nome))
                )
                .ToListAsync();
        }

        // PUT: api/Usuarios/id/5
        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            var existente = await _context.Usuarios.Where(e => e.CID == usuario.CID).SingleOrDefaultAsync();

            if (existente != null)
            {
                return Conflict(new { title = "Conflict", message = $"A record with the same CID already exists." });
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.ID }, usuario);
        }

        // DELETE: api/Usuarios/id/5
        [HttpDelete("id/{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}
