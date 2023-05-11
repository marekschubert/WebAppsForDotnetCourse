using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotNET_lab12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly dotNET_lab10.Data.MyDbContext _context;


        [BindProperty]
        public string selectedCategoryValue { get; set; }
        public IList<Article> Article { get; set; }

        public IndexModel(dotNET_lab10.Data.MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
            if(selectedCategoryValue == null)
            {
                Article = await _context.Articles
                        .Include(a => a.Category)
                        .ToListAsync();
            }
            else
            {
                Article = await _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == int.Parse(selectedCategoryValue)).ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
            if (selectedCategoryValue == null)
            {
                Article = await _context.Articles
                        .Include(a => a.Category)
                        .ToListAsync();
            }
            else
            {
                Article = await _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == int.Parse(selectedCategoryValue)).ToListAsync();
            }
            return Page();

        }


        }
}
