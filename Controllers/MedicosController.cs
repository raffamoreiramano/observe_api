﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Observe.Data;
using Observe.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Observe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "B")]
    public class MedicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            return await _context.Medicos.Include(m => m.Usuario).ToListAsync();
        }

        // GET: api/Medicos/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _context.Medicos.AsQueryable().Where(m => m.ID == id).Include(m => m.Usuario).SingleOrDefaultAsync();
        
            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // GET: api/Medicos/uid/5
        [HttpGet("uid/{uid}")]
        public async Task<ActionResult<Medico>> GetMedicoByUid(int uid)
        {
            var medico = await _context.Medicos.AsQueryable().Where(m => m.UID == uid).Include(m => m.Usuario).SingleOrDefaultAsync();
        
            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // GET: api/Medicos/cid/xyz5
        [HttpGet("cid/{cid}")]
        public async Task<ActionResult<Medico>> GetMedicoByCid(string cid)
        {
            var medico = await _context.Medicos
                .AsQueryable()
                .Include(m => m.Usuario)
                .Where(m => m.Usuario.CID == cid)
                .SingleOrDefaultAsync();
        
            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // GET: api/Medicos/nome/Usuario
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicosByNome(string nome)
        {
            var medicos = await _context.Medicos.AsQueryable().Include(m => m.Usuario)
                .Where(m =>
                    EF.Functions.Like(m.Usuario.Nome, $"%{nome}%")
                    ||
                    EF.Functions.Like(m.Usuario.Sobrenome, $"%{nome}%")
                )
                .ToListAsync();

            if (medicos == null)
            {
                return NotFound();
            }

            return medicos;
        }

        // GET: api/Medicos/id/5/Receitas
        [HttpGet]
        [Route("id/{id}/Receitas")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitas(int id)
        {
            return await _context.Receitas.AsQueryable().Where(r => r.MID == id).ToListAsync();
        }

        // GET: api/Medicos/id/5/Receitas/id/5
        [HttpGet]
        [Route("id/{mid}/Receitas/id/{rid}")]
        public async Task<ActionResult<Receita>> GetReceita(int mid, int rid)
        {
            var receita = await _context.Receitas.AsQueryable().Where(r => r.MID == mid && r.ID == rid)
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

        // PUT: api/Medicos/5
        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutMedico(int id, Medico medico)
        {
            if (id != medico.ID)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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

        // POST: api/Medicos
        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            var usuario = await _context.Medicos.AsQueryable().Where(m => m.UID == medico.UID).SingleOrDefaultAsync();

            if (usuario != null)
            {
                return Conflict(new { title = "Conflict", message = $"A record with the same UID already exists." });
            }

            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedico", new { id = medico.ID }, medico);
        }

        

        // DELETE: api/Medicos/5
        [HttpDelete("id/{id}")]
        public async Task<ActionResult<Medico>> DeleteMedico(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return medico;
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.ID == id);
        }
    }
}
