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
    public class DocumentsController : Controller
    {
        private readonly RECOAppContext _context;

        public DocumentsController(RECOAppContext context)
        {
            _context = context;    
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            List<Document> documents = await _context.Document.ToListAsync();
            foreach(var document in documents)
            {
                document.Pages = await _context.Page.Where(p => p.DocumentId == document.Id).ToListAsync();
            }
            return View(documents);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            document.Pages = await _context.Page.Where(p => p.DocumentId == document.Id).ToListAsync();

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate,CreatedBy")] Document document)
        {
            if (ModelState.IsValid)
            {
                document.Id = Guid.NewGuid();
                document.CreatedDate = DateTime.Now;
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document.SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            document.Pages = await _context.Page.Where(p => p.DocumentId == document.Id).ToListAsync();

            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedDate,CreatedBy")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var document = await _context.Document.SingleOrDefaultAsync(m => m.Id == id);
            foreach (Page page in _context.Page.Where(p => p.DocumentId == document.Id))
            {
                _context.Page.Remove(page);
            }
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DocumentExists(Guid id)
        {
            return _context.Document.Any(e => e.Id == id);
        }
    }
}
