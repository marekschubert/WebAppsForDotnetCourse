using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dotNET_lab10.Data;
using dotNET_lab10.Models;
using System.IO;

namespace dotNET_lab12.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly dotNET_lab10.Data.MyDbContext _dbContext;

        public EditModel(dotNET_lab10.Data.MyDbContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public ArticleViewModel ArticleViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _dbContext
                .Articles
               // .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewData["Image"] = Article.Image;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article article = new Article()
            {
                Id = Article.Id,
                Name = Article.Name,
                Price = Article.Price,
                CategoryId = Article.CategoryId
            };
            if (ArticleViewModel.FormFile != null)
            {
                var guid = Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(ArticleViewModel.FormFile.FileName);
                FileStream fs = new FileStream(_dbContext.UploadFolderPath + guid + fileExtension, FileMode.Create);
                ArticleViewModel.FormFile.CopyTo(fs);
                fs.Close();
                article.Image = _dbContext.UploadFolder + guid + fileExtension;
            }
            else
            {
                var imagePath =  _dbContext.Articles.AsNoTracking().FirstOrDefault(x => x.Id == article.Id).Image;
                if (!(imagePath.Length > 0))
                    article.Image = _dbContext.DefaultImagePath;
                else
                {
                    article.Image = imagePath;
                }
            }

           // _dbContext.Attach(Article).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _dbContext.Update(article);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _dbContext.Articles.Any(e => e.Id == id);
        }
    }
}
