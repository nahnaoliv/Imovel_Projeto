using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImovelAPI.Context;
using ImovelAPI.Models;

namespace ImovelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {
        private readonly Contexto _context;

        public ImovelController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Imovel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetImovel()
        {
            return await _context.Imovel.ToListAsync();
        }

        // GET: api/Imovel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetImovel(int id)
        {
            var imovel = await _context.Imovel.FindAsync(id);

            if (imovel == null)
            {
                return NotFound();
            }

            return imovel;
        }

        // PUT: api/Imovel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImovel(int id, Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return BadRequest();
            }

            _context.Entry(imovel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImovelExists(id))
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

        // POST: api/Imovel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Imovel>> PostImovel(Imovel imovel)
        {
            _context.Imovel.Add(imovel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImovel", new { id = imovel.Id }, imovel);
        }

        // DELETE: api/Imovel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImovel(int id)
        {
            var imovel = await _context.Imovel.FindAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }

            _context.Imovel.Remove(imovel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImovelExists(int id)
        {
            return _context.Imovel.Any(e => e.Id == id);
        }
    }
}
