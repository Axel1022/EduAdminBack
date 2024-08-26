using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduAdmin.Context;
using EduAdmin.Models;
using AutoMapper;
using EduAdmin.DTOs.AdmitidoComo;

namespace EduAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmitidoComoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdmitidoComoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AdmitidoComo
        [HttpGet]
        public async Task<ActionResult<List<AdmitidoComoDTO>>> GetAdmitidoComo()
        {
            var admiticoComo = await _context.AdmitidoComo.ToListAsync();
            admiticoComo = _mapper.Map<List<AdmitidoComo>>(admiticoComo);

            return Ok(admiticoComo);
        }

        // GET: api/AdmitidoComo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmitidoComoDTO>> GetAdmitidoComo(int id)
        {
            var admitidoComo = await _context.AdmitidoComo.FindAsync(id);

            if (admitidoComo == null)
            {
                return NotFound();
            }
            var admitidoComoMap = _mapper.Map<AdmitidoComoDTO>(admitidoComo);

            return admitidoComoMap;
        }

        // PUT: api/AdmitidoComo/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAdmitidoComo(int id, AdmitidoComoCreacionDTO admitidoComoRCreaDTP)
        {
            var existe = await _context.AdmitidoComo.FirstOrDefaultAsync(a => a.Id == id);

            if (existe == null)
            {
                return BadRequest($"No existe admitidoComo con id {id}");
            }

            var existeDescription = await _context.AdmitidoComo.AnyAsync(a => a.Description == admitidoComoRCreaDTP.Description && a.Id != id);

            if (existeDescription)
            {
                return Conflict($"La descripción '{admitidoComoRCreaDTP.Description}' ya existe.");
            }

            _mapper.Map(admitidoComoRCreaDTP, existe);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.AdmitidoComo.Any(e => e.Id == id))
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


        // POST: api/AdmitidoComo
        [HttpPost]
        public async Task<ActionResult<AdmitidoComo>> PostAdmitidoComo(AdmitidoComoCreacionDTO admitidoComo)
        {
            var existe = await _context.AdmitidoComo.FirstOrDefaultAsync(a => a.Description == admitidoComo.Description);

            if (existe != null)
            {
                return Conflict($"{admitidoComo.Description} existe.");
            }

            var AdmitidoComo = _mapper.Map<AdmitidoComo>(admitidoComo);

            _context.Update(AdmitidoComo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/AdmitidoComo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmitidoComo(int id)
        {
            var admitidoComo = await _context.AdmitidoComo.FindAsync(id);
            if (admitidoComo == null)
            {
                return NotFound();
            }

            _context.AdmitidoComo.Remove(admitidoComo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdmitidoComoExists(int id)
        {
            return _context.AdmitidoComo.Any(e => e.Id == id);
        }
    }
}
