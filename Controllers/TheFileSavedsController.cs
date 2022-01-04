using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TheFileSavedsController : Controller
    {
        private readonly WebApplication1Context _context;

        public TheFileSavedsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: TheFileSaveds
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheFileSaved.ToListAsync());
        }

        // GET: TheFileSaveds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theFileSaved = await _context.TheFileSaved
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theFileSaved == null)
            {
                return NotFound();
            }

            return View(theFileSaved);
        }

        // GET: TheFileSaveds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheFileSaveds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UntrustedName,Size,Content")] TheFileSaved theFileSaved)
        {
            if (ModelState.IsValid)
            {
                theFileSaved.Id = Guid.NewGuid();
                theFileSaved.TimeStamp = DateTime.Now;
                _context.Add(theFileSaved);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theFileSaved);
        }

        // GET: TheFileSaveds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theFileSaved = await _context.TheFileSaved.FindAsync(id);
            if (theFileSaved == null)
            {
                return NotFound();
            }
            return View(theFileSaved);
        }

        // POST: TheFileSaveds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UntrustedName,TimeStamp,Size,Content")] TheFileSaved theFileSaved)
        {
            if (id != theFileSaved.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theFileSaved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheFileSavedExists(theFileSaved.Id))
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
            return View(theFileSaved);
        }

        // GET: TheFileSaveds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theFileSaved = await _context.TheFileSaved
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theFileSaved == null)
            {
                return NotFound();
            }

            return View(theFileSaved);
        }

        // POST: TheFileSaveds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var theFileSaved = await _context.TheFileSaved.FindAsync(id);
            _context.TheFileSaved.Remove(theFileSaved);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheFileSavedExists(Guid id)
        {
            return _context.TheFileSaved.Any(e => e.Id == id);
        }
    }
}
