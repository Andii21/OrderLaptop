using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderLaptop.Models.DeviceViewModel;
using OrderLaptop.DeviceModel.Models;
using OrderLaptop.DeviceModel.Data;


namespace OrderLaptop.Controllers
{
    public class SitesController : Controller
    {
        private readonly LibraryContext _context;

        public SitesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index(int? id, int? laptopID)
        {
            var viewModel = new SiteIndexData();
            viewModel.Sites = await _context.Sites

            .Include(i => i.LaptopSites)
            .ThenInclude(i => i.laptop)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.SiteName)
            .ToListAsync();

            if (id != null)
            {
                ViewData["SiteID"] = id.Value;
                Site site = viewModel.Sites.Where(
                i => i.SiteID == id.Value).Single();
                viewModel.Laptops = site.LaptopSites.Select(s => s.laptop);
            }
            if (laptopID != null)
            {
                ViewData["LaptopID"] = laptopID.Value;
                viewModel.Orders = viewModel.Laptops.Where(
                x => x.LaptopID == laptopID).Single().Orders;
            }
            return View(viewModel);

        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .FirstOrDefaultAsync(m => m.SiteID == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteID,SiteName,URL")] Site site)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                 .Include(i => i.LaptopSites).ThenInclude(i => i.laptop)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.SiteID == id);

            if (site == null)
            {
                return NotFound();
            }

            PopulateLaptopSiteData(site);
            return View(site);

        }
        private void PopulateLaptopSiteData(Site site)
        {
            var allLaptops = _context.Laptops;
            var laptopSites = new HashSet<int>(site.LaptopSites.Select(c => c.LaptopID));
            var viewModel = new List<LaptopSiteData>();
            foreach (var laptop in allLaptops)
            {
                viewModel.Add(new LaptopSiteData
                {
                    LaptopID = laptop.LaptopID,
                    Name = laptop.Name,
                    IsInSite = laptopSites.Contains(laptop.LaptopID)
                });
            }
            ViewData["Laptops"] = viewModel;


        }


        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedLaptops)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteToUpdate = await _context.Sites
                .Include(i => i.LaptopSites)
                .ThenInclude(i => i.laptop)
                .FirstOrDefaultAsync(m => m.SiteID == id);

            if (await TryUpdateModelAsync<Site>(
                  siteToUpdate,
                     "",
                 i => i.SiteName, i => i.URL))
            {
                UpdateLaptopSites(selectedLaptops, siteToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateLaptopSites(selectedLaptops, siteToUpdate);
            PopulateLaptopSiteData(siteToUpdate);
            return View(siteToUpdate);
        }
        private void UpdateLaptopSites(string[] selectedLaptops, Site siteToUpdate)
        {
            if (selectedLaptops == null)
            {
                siteToUpdate.LaptopSites = new List<LaptopSite>();
                return;
            }
            var selectedLaptopsHS = new HashSet<string>(selectedLaptops);
            var laptopSites = new HashSet<int>
            (siteToUpdate.LaptopSites.Select(c => c.laptop.LaptopID));

            foreach (var laptop in _context.Laptops)
            {
                if (selectedLaptopsHS.Contains(laptop.LaptopID.ToString()))
                {
                    if (!laptopSites.Contains(laptop.LaptopID))
                    {
                        siteToUpdate.LaptopSites.Add(new LaptopSite
                        {
                            SiteID =
                       siteToUpdate.SiteID,
                            LaptopID = laptop.LaptopID
                        });
                    }
                }
                else
                {
                    if (laptopSites.Contains(laptop.LaptopID))
                    {
                        LaptopSite laptopToRemove = siteToUpdate.LaptopSites.FirstOrDefault(i
                       => i.LaptopID == laptop.LaptopID);
                        _context.Remove(laptopToRemove);
                    }
                }
            }
        }


    // GET: Sites/Delete/5
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .FirstOrDefaultAsync(m => m.SiteID == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.SiteID == id);
        }
    }
}
