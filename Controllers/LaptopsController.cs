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
    public class LaptopsController : Controller
    {
        private readonly LibraryContext _context;

        public LaptopsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Laptops
        public async Task<IActionResult> Index(string sortOrder,
                                                string currentFilter,                                    
                                                string searchString, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var laptops = from b in _context.Laptops
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                laptops = laptops.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    laptops = laptops.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    laptops = laptops.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    laptops = laptops.OrderByDescending(b => b.Price);
                    break;
                default:
                    laptops = laptops.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Laptop>.CreateAsync(laptops.AsNoTracking(), pageNumber ??1, pageSize));
        }
    

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LaptopID == id);

            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] Laptop laptop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(laptop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Laptops.FirstOrDefaultAsync(s=>s.LaptopID==id);
              
            if (await TryUpdateModelAsync<Laptop>(
                  studentToUpdate,
                  "",
                  s=> s.Name, s=>s.Description, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException/*ex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists");
                }
            }
           
            return View(studentToUpdate);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptops
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LaptopID == id);
            if (laptop == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await _context.Laptops.FindAsync(id);
            if(laptop == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {

                _context.Laptops.Remove(laptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /*ex*/) {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool LaptopExists(int id)
        {
            return _context.Laptops.Any(e => e.LaptopID == id);
        }
    }
}
