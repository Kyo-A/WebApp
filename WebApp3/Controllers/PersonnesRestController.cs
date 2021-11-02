using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp3.Models;
using WebApp3.Utils;

namespace WebApp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class PersonnesRestController : ControllerBase
    {
        private readonly ModelDbContext _context;

        public PersonnesRestController(ModelDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonnesRest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonne()
        {
            // return await _context.Personne.ToListAsync();
            return Ok(new ApiOkResponse(await _context.Personne.ToListAsync()));
        }

        // GET: api/PersonnesRest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(int id)
        {
            var personne = await _context.Personne.FindAsync(id);

            if (personne == null)
            {
                return NotFound(new ApiResponse(404, $"Person not found with id {id} "));
            }
            //return Ok(personne);

           return Ok(new ApiOkResponse(personne));
        }

        // PUT: api/PersonnesRest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            _context.Entry(personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneExists(id))
                {
                    return NotFound(new ApiResponse(404, $"Person not found with id {id} "));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PersonnesRest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            _context.Personne.Add(personne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonne", new { id = personne.Num }, personne);
        }

        // DELETE: api/PersonnesRest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = await _context.Personne.FindAsync(id);
            if (personne == null)
            {
                return NotFound(new ApiResponse(404, $"Person not found with id {id} "));
            }

            _context.Personne.Remove(personne);
            await _context.SaveChangesAsync();

            return Ok(new ApiOkResponse(personne));
        }

        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.Num == id);
        }
    }
}
