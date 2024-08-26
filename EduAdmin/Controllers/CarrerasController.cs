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
using EduAdmin.DTOs.Carrera;

namespace EduAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CarrerasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Carreras
        [HttpGet]
        public async Task<ActionResult<List<CarreraDTO>>> GetCarrera()
        {
            var carreras = await _context.Carrera.ToListAsync();
            return Ok(_mapper.Map<List<CarreraDTO>>(carreras));
        }

        // GET: api/Carreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarreraDTO>> GetCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);

            if (carrera == null)
            {
                return NotFound($"No existe una carrera con id {id}");
            }

            return Ok(_mapper.Map<CarreraDTO>(carrera));
        }

        // PUT: api/Carreras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCarrera(int id, CarreraCreacionDTO carreraCreacionDTO)
        {
            if (!CarreraExists(id))
            {
                return NotFound($"No existe una carrera con id {id}");
            }

            var carreraMap = _mapper.Map<Carrera>(carreraCreacionDTO);
            carreraMap.Id= id;
            
            _context.Carrera.Update(carreraMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Carreras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(CarreraCreacionDTO carreraCreacionDTO)
        {
            var existeNombre = await _context.Carrera.AnyAsync(c => c.Name == carreraCreacionDTO.Name);
            if (existeNombre)
            {
                return NotFound($"{carreraCreacionDTO.Name} existe.");
            }
            var carreraMap = _mapper.Map<Carrera>(carreraCreacionDTO);

            _context.Carrera.Update(carreraMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Carreras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound($"No existe una carrera con id {id}");

            }

            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarreraExists(int id)
        {
            return _context.Carrera.Any(e => e.Id == id);
        }
    }
}
