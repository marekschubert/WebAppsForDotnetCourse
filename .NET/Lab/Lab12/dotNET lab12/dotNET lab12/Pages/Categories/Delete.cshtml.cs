using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotNET_lab10.Models;

namespace dotNET_lab12.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly dotNET_lab10.Data.MyDbContext _dbContext;

        public DeleteModel(dotNET_lab10.Data.MyDbContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _dbContext.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _dbContext.Categories.FindAsync(id);

            if (Category != null)
            {
                var articlesToDelete = _dbContext.Articles.Where(x => x.CategoryId == Category.Id).ToList();
                foreach (Article article in articlesToDelete)
                {
                    if (!article.Image.Equals(_dbContext.DefaultImagePath))
                    {
                        System.IO.File.Delete("wwwroot/" + article.Image);
                    }
                }
                _dbContext.Categories.Remove(Category);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
