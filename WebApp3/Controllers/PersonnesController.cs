using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp3.Models;

namespace WebApp3.Controllers
{
    public class PersonnesController : Controller
    {
        private readonly ModelDbContext _context;

        public PersonnesController(ModelDbContext context)
        {
            _context = context;
        }

        // GET: Personnes
        [HttpGet]
        [Route("personnes/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personne.ToListAsync());
        }

        // GET: Personnes/Details/5
        [HttpGet]
        [Route("personnes/index/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne
                .FirstOrDefaultAsync(m => m.Num == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // GET: Personnes/Create
        [HttpGet]
        [Route("personnes/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("personnes/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Nom,Prenom")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }

        // GET: Personnes/Edit/5
        [HttpGet]
        [Route("personnes/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("personnes/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num,Nom,Prenom")] Personne personne)
        {
            if (id != personne.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(personne.Num))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }

        // GET: Personnes/Delete/5
        [HttpGet]
        [Route("personnes/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne
                .FirstOrDefaultAsync(m => m.Num == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("personnes/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personne = await _context.Personne.FindAsync(id);
            _context.Personne.Remove(personne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.Num == id);
        }
    }
}
