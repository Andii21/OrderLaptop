using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderLaptop.Data;
using OrderLaptop.Models;

namespace OrderLaptop.Controllers
{
    public class LaptopSitesController : Controller
    {
        private readonly LibraryContext _context;

        public LaptopSitesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: LaptopSites
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.LaptopSites.Include(l => l.laptop).Include(l => l.site);
            return View(await libraryContext.ToListAsync());
        }

        // GET: LaptopSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopSite = await _context.LaptopSites
                .Include(l => l.laptop)
                .Include(l => l.site)
                .FirstOrDefaultAsync(m => m.LaptopID == id);
            if (laptopSite == null)
            {
                return NotFound();
            }

            return View(laptopSite);
        }

        // GET: LaptopSites/Create
        public IActionResult Create()
        {
            ViewData["LaptopID"] = new SelectList(_context.Laptops, "LaptopID", "LaptopID");
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteID");
            return View();
        }

        // POST: LaptopSites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaptopID,SiteID")] LaptopSite laptopSite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptopSite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LaptopID"] = new SelectList(_context.Laptops, "LaptopID", "LaptopID", laptopSite.LaptopID);
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteID", laptopSite.SiteID);
            return View(laptopSite);
        }

        // GET: LaptopSites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopSite = await _context.LaptopSites.FindAsync(id);
            if (laptopSite == null)
            {
                return NotFound();
            }
            ViewData["LaptopID"] = new SelectList(_context.Laptops, "LaptopID", "LaptopID", laptopSite.LaptopID);
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteID", laptopSite.SiteID);
            return View(laptopSite);
        }

        // POST: LaptopSites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LaptopID,SiteID")] LaptopSite laptopSite)
        {
            if (id != laptopSite.LaptopID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptopSite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopSiteExists(laptopSite.LaptopID))
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
            ViewData["LaptopID"] = new SelectList(_context.Laptops, "LaptopID", "LaptopID", laptopSite.LaptopID);
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteID", laptopSite.SiteID);
            return View(laptopSite);
        }

        // GET: LaptopSites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopSite = await _context.LaptopSites
                .Include(l => l.laptop)
                .Include(l => l.site)
                .FirstOrDefaultAsync(m => m.LaptopID == id);
            if (laptopSite == null)
            {
                return NotFound();
            }

            return View(laptopSite);
        }

        // POST: LaptopSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptopSite = await _context.LaptopSites.FindAsync(id);
            _context.LaptopSites.Remove(laptopSite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopSiteExists(int id)
        {
            return _context.LaptopSites.Any(e => e.LaptopID == id);
        }
    }
}
