using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stajbul.Data;
using stajbul.Models;

namespace stajbul.Controllers
{
    public class talentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public talentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: talents
        public async Task<IActionResult> Index()
        {
            return View(await _context.talents.ToListAsync());
        }

        // GET: talents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talents = await _context.talents
                .FirstOrDefaultAsync(m => m.id == id);
            if (talents == null)
            {
                return NotFound();
            }

            return View(talents);
        }

        // GET: talents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: talents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] talents talents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talents);
        }

        // GET: talents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talents = await _context.talents.FindAsync(id);
            if (talents == null)
            {
                return NotFound();
            }
            return View(talents);
        }

        // POST: talents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name")] talents talents)
        {
            if (id != talents.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!talentsExists(talents.id))
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
            return View(talents);
        }

        // GET: talents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talents = await _context.talents
                .FirstOrDefaultAsync(m => m.id == id);
            if (talents == null)
            {
                return NotFound();
            }

            return View(talents);
        }

        // POST: talents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talents = await _context.talents.FindAsync(id);
            _context.talents.Remove(talents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool talentsExists(int id)
        {
            return _context.talents.Any(e => e.id == id);
        }
    }
}
