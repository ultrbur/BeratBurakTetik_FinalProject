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
    public class talentsofJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public talentsofJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: talentsofJobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.talentsofJobs.Include(t => t.jobPosting).Include(t => t.talent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: talentsofJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talentsofJobs = await _context.talentsofJobs
                .Include(t => t.jobPosting)
                .Include(t => t.talent)
                .FirstOrDefaultAsync(m => m.id == id);
            if (talentsofJobs == null)
            {
                return NotFound();
            }

            return View(talentsofJobs);
        }

        // GET: talentsofJobs/Create
        public IActionResult Create()
        {
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title");
            ViewData["talentID"] = new SelectList(_context.talents, "id", "name");
            return View();
        }

        // POST: talentsofJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,talentID,jobPostingID")] talentsofJobs talentsofJobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talentsofJobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", talentsofJobs.jobPostingID);
            ViewData["talentID"] = new SelectList(_context.talents, "id", "name", talentsofJobs.talentID);
            return View(talentsofJobs);
        }

        // GET: talentsofJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talentsofJobs = await _context.talentsofJobs.FindAsync(id);
            if (talentsofJobs == null)
            {
                return NotFound();
            }
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", talentsofJobs.jobPostingID);
            ViewData["talentID"] = new SelectList(_context.talents, "id", "name", talentsofJobs.talentID);
            return View(talentsofJobs);
        }

        // POST: talentsofJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,talentID,jobPostingID")] talentsofJobs talentsofJobs)
        {
            if (id != talentsofJobs.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talentsofJobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!talentsofJobsExists(talentsofJobs.id))
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
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", talentsofJobs.jobPostingID);
            ViewData["talentID"] = new SelectList(_context.talents, "id", "name", talentsofJobs.talentID);
            return View(talentsofJobs);
        }

        // GET: talentsofJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talentsofJobs = await _context.talentsofJobs
                .Include(t => t.jobPosting)
                .Include(t => t.talent)
                .FirstOrDefaultAsync(m => m.id == id);
            if (talentsofJobs == null)
            {
                return NotFound();
            }

            return View(talentsofJobs);
        }

        // POST: talentsofJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talentsofJobs = await _context.talentsofJobs.FindAsync(id);
            _context.talentsofJobs.Remove(talentsofJobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool talentsofJobsExists(int id)
        {
            return _context.talentsofJobs.Any(e => e.id == id);
        }
    }
}
