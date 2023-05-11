using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using dotNET_lab10.Data;
using dotNET_lab10.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace dotNET_lab12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly dotNET_lab10.Data.MyDbContext _dbContext;

        public CreateModel(dotNET_lab10.Data.MyDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public ArticleViewModel ArticleViewModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Article article = new Article()
            {
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
                article.Image = _dbContext.DefaultImagePath;
            }
            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
