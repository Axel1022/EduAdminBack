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
using EduAdmin.DTOs.Estudiante;

namespace EduAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EstudiantesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<List<EstudianteDTO>>> GetEstudiante()
        {
            var estudiantes= await _context.Estudiante.Include(e => e.Carrera).ToListAsync();

            return Ok(_mapper.Map<List<EstudianteDTO>>(estudiantes));
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteDTO>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return _mapper.Map<EstudianteDTO>(estudiante);
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEstudiante(int id, [FromBody] EstudianteCreacionDto estudiante)
        {
            if (!EstudianteExists(id))
            {
                return NotFound($"No existe un estudiante con id {id}");
            }

            var existeCarrera = await _context.Carrera.AnyAsync(x => x.Id == estudiante.IdCarrera);

            if (!existeCarrera)
            {
                return NotFound($"Carrera con id {estudiante.IdCarrera} no encontrada.");
            }

            var existeAdmitidoComo = await _context.AdmitidoComo.AnyAsync(x => x.Id == estudiante.IdAdmitidoComo);

            if (!existeAdmitidoComo)
            {
                return NotFound($"AdmitidoComo con id {estudiante.IdAdmitidoComo} no encontrado.");
            }

            var estudianteMap = _mapper.Map<Estudiante>(estudiante);
            estudianteMap.Id = id;

            _context.Update(estudianteMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Estudiantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(EstudianteCreacionDto estudianteCreacioDTO)
        {
            if (EstudianteExists(estudianteCreacioDTO.Name))
            {
                return Conflict($"{estudianteCreacioDTO.Name} existe.");
            }

            var existeCarrera = await _context.Carrera.AnyAsync(x => x.Id == estudianteCreacioDTO.IdCarrera);

            if (!existeCarrera)
            {
                return NotFound($"Carrera con id {estudianteCreacioDTO.IdCarrera} no encontrada.");
            }

            var existeAdmitidoComo = await _context.AdmitidoComo.AnyAsync(x => x.Id == estudianteCreacioDTO.IdAdmitidoComo);

            if (!existeAdmitidoComo)
            {
                return NotFound($"AdmitidoComo con id {estudianteCreacioDTO.IdAdmitidoComo} no encontrado.");
            }

            var estudiante = _mapper.Map<Estudiante>(estudianteCreacioDTO);

            _context.Estudiante.Add(estudiante);
            await _context.SaveChangesAsync();

            return Created();
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.Id == id);
        }
        private bool EstudianteExists(string nombre)
        {
            return _context.Estudiante.Any(e => e.Name == nombre);
        }
    }
}
