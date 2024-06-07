using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Developers.Models;

namespace Developers.Controllers
{
    public class DescripcionserviciosController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public DescripcionserviciosController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Descripcionservicios
        public async Task<IActionResult> Index()
        {
            var mercyDeveloperContext = _context.Descripcionservicios.Include(d => d.IdServicioNavigation);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Descripcionservicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios
                .Include(d => d.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDs == id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }

            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Create
        public IActionResult Create()
        {
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio");
            return View();
        }

        // POST: Descripcionservicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDs,Nombre,IdServicio")] Descripcionservicio descripcionservicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descripcionservicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.IdServicio);
            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.IdServicio);
            return View(descripcionservicio);
        }

        // POST: Descripcionservicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDs,Nombre,IdServicio")] Descripcionservicio descripcionservicio)
        {
            if (id != descripcionservicio.IdDs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descripcionservicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionservicioExists(descripcionservicio.IdDs))
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
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.IdServicio);
            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios
                .Include(d => d.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDs == id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }

            return View(descripcionservicio);
        }

        // POST: Descripcionservicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descripcionservicio = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicio != null)
            {
                _context.Descripcionservicios.Remove(descripcionservicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescripcionservicioExists(int id)
        {
            return _context.Descripcionservicios.Any(e => e.IdDs == id);
        }
    }
}
