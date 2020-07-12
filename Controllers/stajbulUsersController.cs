using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stajbul.Data;
using stajbul.Models;

namespace stajbul.Controllers
{
    public class stajbulUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public stajbulUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: stajbulUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.stajbulUser.ToListAsync());
        }

        // GET: stajbulUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stajbulUser = await _context.stajbulUser
                .FirstOrDefaultAsync(m => m.id == id);
            if (stajbulUser == null)
            {
                return NotFound();
            }

            return View(stajbulUser);
        }

        // GET: stajbulUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: stajbulUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,fullname,birthdate,school,department,isAdmin")] stajbulUser stajbulUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stajbulUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stajbulUser);
        }

        // GET: stajbulUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stajbulUser = await _context.stajbulUser.FindAsync(id);
            if (stajbulUser == null)
            {
                return NotFound();
            }
            return View(stajbulUser);
        }

        // POST: stajbulUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,fullname,birthdate,school,department,isAdmin")] stajbulUser stajbulUser)
        {
            if (id != stajbulUser.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stajbulUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!stajbulUserExists(stajbulUser.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","jobPostings");
            }
            return View(stajbulUser);
        }

        // GET: stajbulUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stajbulUser = await _context.stajbulUser
                .FirstOrDefaultAsync(m => m.id == id);
            if (stajbulUser == null)
            {
                return NotFound();
            }

            return View(stajbulUser);
        }

        // POST: stajbulUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stajbulUser = await _context.stajbulUser.FindAsync(id);
            _context.stajbulUser.Remove(stajbulUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool stajbulUserExists(int id)
        {
            return _context.stajbulUser.Any(e => e.id == id);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(stajbulUser user)
        {
            if (ModelState.IsValid)
            {
                string SessionId = "id";
                string SessionName = "name";
                string SessionAdmin = "admin";
                var v = _context.stajbulUser.Where(a => a.username.Equals(user.username) && a.password.Equals(user.password)).FirstOrDefault();
                if (v != null)
                {
                    HttpContext.Session.SetInt32(SessionId, v.id);
                    HttpContext.Session.SetString(SessionName, v.username.ToString());
                    HttpContext.Session.SetString(SessionAdmin, v.isAdmin.ToString());
                    return View("Details",v);
                }

            }
            return View(user);
        }
        public async Task<IActionResult> Profile()
        {
            var id = HttpContext.Session.GetInt32("id");

            if (id == null)
            {
                return NotFound();
            }

            var user = _context.stajbulUser.Where(u => u.id== id);
            
            if (user== null)
            {
                return NotFound();
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
