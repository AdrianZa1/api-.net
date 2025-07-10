using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LecturasApi.Data;
using LecturasApi.Models;
using LecturasApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LecturasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LecturasController(AppDbContext context)
        {
            _context = context;
        }

        // DTO para respuesta
        public class LecturaResponseDTO
        {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public int MedidorId { get; set; }
        public int NumeroMedidor { get; set; }
        public string TipoMedidor { get; set; }
        }

        // GET: api/lecturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturaResponseDTO>>> GetLecturas()
        {
            var dtos = await (from l in _context.Lecturas
                              join m in _context.Medidores on l.MedidorId equals m.Id
                              select new LecturaResponseDTO
                              {
                                  Id = l.Id,
                                  Fecha = l.Fecha,
                                  Valor = l.Valor,
                                  MedidorId = l.MedidorId,
                                  NumeroMedidor = m.Numero,
                                  TipoMedidor = m.Tipo
                              }).ToListAsync();
            return Ok(dtos);
        }

        // GET: api/lecturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturaResponseDTO>> GetLectura(int id)
        {
            var lectura = await _context.Lecturas
                .Include(l => l.Medidor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lectura == null) return NotFound();

            var dto = new LecturaResponseDTO
            {
                Id = lectura.Id,
                Fecha = lectura.Fecha,
                Valor = lectura.Valor,
                MedidorId = lectura.MedidorId,
                NumeroMedidor = lectura.Medidor.Numero,
                TipoMedidor = lectura.Medidor.Tipo
            };

            return Ok(dto);
        }

        // POST: api/lecturas
        [HttpPost]
        public async Task<ActionResult<LecturaResponseDTO>> PostLectura([FromBody] LecturaDTO lecturaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var medidor = await _context.Medidores.FindAsync(lecturaDto.MedidorId);
            if (medidor == null)
                return NotFound($"No se encontró el medidor con ID {lecturaDto.MedidorId}");

            var lectura = new Lectura
            {
                Fecha = lecturaDto.Fecha,
                Valor = lecturaDto.Valor,
                MedidorId = lecturaDto.MedidorId
            };

            _context.Lecturas.Add(lectura);
            await _context.SaveChangesAsync();

            var responseDto = new LecturaResponseDTO
            {
                Id = lectura.Id,
                Fecha = lectura.Fecha,
                Valor = lectura.Valor,
                MedidorId = lectura.MedidorId,
                NumeroMedidor = medidor.Numero,
                TipoMedidor = medidor.Tipo
            };

            return CreatedAtAction(nameof(GetLectura), new { id = lectura.Id }, responseDto);
        }

        // PUT: api/lecturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectura(int id, [FromBody] LecturaDTO lecturaDto)
        {
            var lectura = await _context.Lecturas.FindAsync(id);
            if (lectura == null) return NotFound();

            var medidor = await _context.Medidores.FindAsync(lecturaDto.MedidorId);
            if (medidor == null)
                return NotFound($"No se encontró el medidor con ID {lecturaDto.MedidorId}");

            lectura.Fecha = lecturaDto.Fecha;
            lectura.Valor = lecturaDto.Valor;
            lectura.MedidorId = lecturaDto.MedidorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar la lectura.");
            }

            return NoContent();
        }

        // DELETE: api/lecturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectura(int id)
        {
            var lectura = await _context.Lecturas.FindAsync(id);
            if (lectura == null) return NotFound();

            _context.Lecturas.Remove(lectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
