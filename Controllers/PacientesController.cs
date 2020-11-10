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
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _context.Pacientes.Include(p => p.Usuario).ToListAsync();
        }

        // GET: api/Pacientes/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await 
                _context
                .Pacientes
                .AsQueryable()
                .Where(p => p.ID == id)
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        // GET: api/Pacientes/uid/5
        [HttpGet("uid/{uid}")]
        public async Task<ActionResult<Paciente>> GetPacienteByUid(int uid)
        {
            var paciente = await 
                _context
                .Pacientes
                .AsQueryable()
                .Where(p => p.UID == uid)
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        // GET: api/Pacientes/nome/Usuario
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesByNome(string nome)
        {
            var pacientes = await 
                _context
                .Pacientes
                .AsQueryable()
                .Include(p => p.Usuario)
                .Where(p =>
                    EF.Functions.Like(p.Usuario.Nome, $"%{nome}%")
                    ||
                    EF.Functions.Like(p.Usuario.Sobrenome, $"%{nome}%")
                )
                .ToListAsync();

            if (pacientes == null)
            {
                return NotFound();
            }

            return pacientes;
        }

        // GET: api/Pacientes/id/5/Receitas
        [HttpGet]
        [Route("id/{id}/Receitas")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitas(int id)
        {
            return await _context.Receitas.Where(r => r.PID == id).ToListAsync();
        }

        // GET: api/Pacientes/id/5/Receitas/id/5
        [HttpGet]
        [Route("id/{pid}/Receitas/id/{rid}")]
        public async Task<ActionResult<Receita>> GetReceita(int pid, int rid)
        {
            var receita = await _context.Receitas.AsQueryable()
                .Where(r => r.PID == pid && r.ID == rid)
                .Include(r => r.Medico)
                    .ThenInclude(m => m.Usuario)
                .Include(r => r.Paciente)
                    .ThenInclude(p => p.Usuario)
                .SingleOrDefaultAsync();

            if (receita == null)
            {
                return NotFound();
            }

            return receita;
        }

        // PUT: api/Pacientes/id/5
        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            var usuario = await _context.Pacientes.AsQueryable().Where(p => p.UID == paciente.UID).SingleOrDefaultAsync();

            if (usuario != null)
            {
                return Conflict(new { title = "Conflict", message = $"A record with the same UID already exists." });
            }

            if (id != paciente.ID)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            var usuario = await _context.Pacientes.AsQueryable().Where(p => p.UID == paciente.UID).SingleOrDefaultAsync();

            if (usuario != null)
            {
                return Conflict(new { title = "Conflict", message = $"A record with the same UID already exists." });
            }

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.ID }, paciente);
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("id/{id}")]
        public async Task<ActionResult<Paciente>> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }
    }
}