using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using stajbul.Data;
using stajbul.Models;

namespace stajbul.Controllers
{
    public class jobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public jobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: jobApplications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.jobApplications.Include(j => j.jobPosting).Include(j => j.stajbulUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: jobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplications = await _context.jobApplications
                .Include(j => j.jobPosting)
                .Include(j => j.stajbulUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (jobApplications == null)
            {
                return NotFound();
            }

            return View(jobApplications);
        }

        // GET: jobApplications/Create
        public IActionResult Create()
        {
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title");
            ViewData["stajbulUserID"] = new SelectList(_context.stajbulUser, "id", "password");
            return View();
        }

        // POST: jobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,stajbulUserID,jobPostingID")] jobApplications jobApplications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobApplications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", jobApplications.jobPostingID);
            ViewData["stajbulUserID"] = new SelectList(_context.stajbulUser, "id", "username", jobApplications.stajbulUserID);
            return View(jobApplications);
        }

        // GET: jobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplications = await _context.jobApplications.FindAsync(id);
            if (jobApplications == null)
            {
                return NotFound();
            }
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", jobApplications.jobPosting);
            ViewData["stajbulUserID"] = new SelectList(_context.stajbulUser, "id", "username", jobApplications.stajbulUser);
            return View(jobApplications);
        }

        // POST: jobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,stajbulUserID,jobPostingID")] jobApplications jobApplications)
        {
            if (id != jobApplications.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jobApplicationsExists(jobApplications.id))
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
            ViewData["jobPostingID"] = new SelectList(_context.jobPosting, "id", "title", jobApplications.jobPostingID);
            ViewData["stajbulUserID"] = new SelectList(_context.stajbulUser, "id", "password", jobApplications.stajbulUserID);
            return View(jobApplications);
        }

        // GET: jobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplications = await _context.jobApplications
                .Include(j => j.jobPosting)
                .Include(j => j.stajbulUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (jobApplications == null)
            {
                return NotFound();
            }

            return View(jobApplications);
        }

        // POST: jobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplications = await _context.jobApplications.FindAsync(id);
            _context.jobApplications.Remove(jobApplications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jobApplicationsExists(int id)
        {
            return _context.jobApplications.Any(e => e.id == id);
        }
     
        public IActionResult ApplyJob(int jobid,jobApplications jobApplications)
        {
            jobApplications.jobPostingID = jobid;
            var userid = HttpContext.Session.GetInt32("id");
            jobApplications.stajbulUserID = userid.Value;
                _context.Add(jobApplications);
                //_context.SaveChanges();
                return RedirectToAction("Index","jobPostings");
        }

    }
}
