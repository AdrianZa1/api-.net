using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LecturasApi.Data;
using LecturasApi.Models;
using LecturasApi.DTOs;  // Importar el namespace de DTOs

namespace LecturasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedidoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MedidoresController(AppDbContext context) => _context = context;

        // GET: api/medidores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medidor>>> GetMedidores() =>
            await _context.Medidores.ToListAsync();

        // GET: api/medidores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medidor>> GetMedidor(int id)
        {
            var medidor = await _context.Medidores.FindAsync(id);
            if (medidor == null) return NotFound();
            return medidor;
        }

        // POST: api/medidores
        [HttpPost]
        public async Task<ActionResult<Medidor>> PostMedidor([FromBody] MedidorDTO medidorDto)
        {
            // Mapear manualmente MedidorDTO a Medidor
            var medidor = new Medidor
            {
                Numero = medidorDto.Numero,
                Tipo = medidorDto.Tipo,
                ClienteId = medidorDto.ClienteId // Usar int directamente
            };

            _context.Medidores.Add(medidor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedidor), new { id = medidor.Id }, medidor);
        }

        // PUT: api/medidores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedidor(int id, Medidor medidor)
        {
            if (id != medidor.Id) return BadRequest();
            _context.Entry(medidor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Medidores.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/medidores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedidor(int id)
        {
            var medidor = await _context.Medidores.FindAsync(id);
            if (medidor == null) return NotFound();

            _context.Medidores.Remove(medidor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
