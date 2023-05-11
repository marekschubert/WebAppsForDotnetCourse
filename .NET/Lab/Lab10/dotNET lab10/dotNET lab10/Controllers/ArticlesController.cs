using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace dotNET_lab10.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        private string uploadFolderPath;
        private string uploadFolder;
        private string defaultImagePath;

        public ArticlesController(MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            uploadFolderPath = "wwwroot/upload/";
            uploadFolder = "upload/";
            defaultImagePath = "image/noimage.png";
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Articles.Include(a => a.Category);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleViewModel articleViewModel)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article()
                {
                    Name = articleViewModel.Name,
                    Price = articleViewModel.Price,
                    CategoryId = articleViewModel.CategoryId
                };
                if(articleViewModel.FormFile != null)
                {
                    var guid = Guid.NewGuid().ToString();
                    var fileExtension = Path.GetExtension(articleViewModel.FormFile.FileName);
                    articleViewModel.FormFile.CopyTo(new FileStream(uploadFolderPath + guid + fileExtension, FileMode.Create));
                    article.Image = uploadFolder + guid + fileExtension;
                } 
                else
                {
                    article.Image = defaultImagePath;
                }

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", articleViewModel.CategoryId);
            return View(articleViewModel);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);

            ArticleViewModel articleViewModel = new ArticleViewModel()
            {
                Name = article.Name,
                CategoryId = article.CategoryId,
                Price = article.Price
            };
            ViewData["Image"] = article.Image;
            return View(articleViewModel);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticleViewModel articleViewModel)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article()
                {
                    Id = id,
                    Name = articleViewModel.Name,
                    Price = articleViewModel.Price,
                    CategoryId = articleViewModel.CategoryId
                };
                if (articleViewModel.FormFile != null)
                {
                    var guid = Guid.NewGuid().ToString();
                    var fileExtension = Path.GetExtension(articleViewModel.FormFile.FileName);
                    FileStream fs = new FileStream(uploadFolderPath + guid + fileExtension, FileMode.Create);
                    articleViewModel.FormFile.CopyTo(fs);
                    fs.Close();
                    article.Image = uploadFolder + guid + fileExtension;
                }
                else
                {
                    var imagePath = _context.Articles.AsNoTracking().FirstOrDefault(x => x.Id == article.Id).Image;
                    if (!(imagePath.Length > 0))
                        article.Image = defaultImagePath;
                    else
                    {
                        article.Image = imagePath;
                    }
                }
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", articleViewModel.CategoryId);
            return View(articleViewModel);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = _context.Articles.Find(id);
            //////
            
            if (!article.Image.Equals(defaultImagePath))
            {
                System.IO.File.Delete("wwwroot/" + article.Image);
            }  
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
