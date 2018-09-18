using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RECOApp.Models;

namespace RECOApp.Controllers
{
    public class PagesController : Controller
    {
        private readonly RECOAppContext _context;

        public PagesController(RECOAppContext context)
        {
            _context = context;    
        }

        // GET: Pages
        public async Task<IActionResult> Index(Guid documentId)
        {
            if (!DocumentExists(documentId))
            {
                return NotFound();
            }

            ViewBag.DocumentId = documentId;
            return View(await _context.Page.Where(p => p.DocumentId == documentId).ToListAsync());
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page.SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create(Guid documentId)
        {
            if (!DocumentExists(documentId))
            {
                return NotFound();
            }

            ViewBag.DocumentId = documentId;
            ViewBag.LastPageNumber = getLastPageNumber(documentId);
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentId,Number,Content,CreatedBy")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Id = Guid.NewGuid();
                page.CreatedDate = DateTime.Now;
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { documentId = page .DocumentId });
            }

            ViewBag.DocumentId = page.DocumentId;
            ViewBag.LastPageNumber = getLastPageNumber(page.DocumentId);

            return View(page);
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page.SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DocumentId,Number,Content,CreatedDate,CreatedBy")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { documentId = page.DocumentId });
            }
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page
                .SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var page = await _context.Page.SingleOrDefaultAsync(m => m.Id == id);
            _context.Page.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { documentId = page.DocumentId });
        }

        private bool PageExists(Guid id)
        {
            return _context.Page.Any(e => e.Id == id);
        }

        private int getLastPageNumber(Guid documentId)
        {
            return _context.Page.Where(e => e.DocumentId == documentId).Select(p => p.Number).Max();
        }

        private bool DocumentExists(Guid id)
        {
            return _context.Document.Any(e => e.Id == id);
        }
    }
}
