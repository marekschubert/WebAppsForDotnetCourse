using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace dotNET_lab10.Controllers
{
    [Authorize(Roles = "User")]
    public class ShopController : Controller
    {
        public const int WEEK = 3600 * 24 * 7;

        private readonly MyDbContext _context;
        public ShopController(MyDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // [Authorize(Roles = "User")]
        public ActionResult AddArticleToCart(int id)
        {
            if(Request.Cookies.ContainsKey(CreateArticleCoookieKey(id)))
            {
                SetCookie(CreateArticleCoookieKey(id), (int.Parse(Request.Cookies[CreateArticleCoookieKey(id)]) + 1).ToString());
            }
            else
            {
                SetCookie(CreateArticleCoookieKey(id), "1");
            }
            return RedirectToAction(nameof(CartView));            
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "User")]
        public ActionResult RemoveArticleFromCart(int id)
        {
            if (Request.Cookies.ContainsKey(CreateArticleCoookieKey(id)))
            {
                SetCookie(CreateArticleCoookieKey(id), (int.Parse(Request.Cookies[CreateArticleCoookieKey(id)]) - 1).ToString());
                if (Request.Cookies[CreateArticleCoookieKey(id)] == "1")
                {
                    Response.Cookies.Delete(CreateArticleCoookieKey(id));
                }
            }
            return RedirectToAction(nameof(CartView));
        }

        // [Authorize(Roles = "User")]
        private IEnumerable<CartArticleViewModel> GetCartArticles()
        {
            var cookies = HttpContext.Request.Cookies;

            var articlesIds = cookies.Select(x => 
                                {
                                    var result = default(int);
                                    if(int.TryParse(x.Key.ToString().Remove(0, "article".Length), out result))
                                        return int.Parse(x.Key.ToString().Remove(0, "article".Length));
                                    return -1;
                                });

            var cartArticles = _context.Articles.Include(a => a.Category).Where(x => articlesIds.Contains(x.Id)).ToList();

            var cartArticlesWithQuantity = cartArticles.Select(x => new CartArticleViewModel
            {
                Article = x,
                Quantity = int.Parse(cookies["article" + x.Id])
            }).ToList();

            return cartArticlesWithQuantity;
        }

        private void ClearCartCookies()
        {
            var cartCookiesKeys = HttpContext.Request.Cookies.Keys.Where(k => k.ToString().Contains("article"));

            foreach (var cookieKey in cartCookiesKeys)
            {
                HttpContext.Response.Cookies.Delete(cookieKey);
            }
        }

        [AllowAnonymous]
        //[Authorize(Policy = "AdminNotAllowedToBasket")]
        [Authorize(Roles = "User")]
        public IActionResult CartView()
        {
            var cartArticlesWithQuantity = GetCartArticles();

            return View(cartArticlesWithQuantity);
        }

        [HttpGet]
        public IActionResult CreateNewOrder()
        {
            var cartArticlesWithQuantity = GetCartArticles();

            return View(cartArticlesWithQuantity);
        }

     //   [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult CreateNewOrder(string fullName, string street, string postalCode, string city, string phoneNumber, string deliveryOptions, string paymentOptions)
        {
            var cartArticlesWithQuantity = GetCartArticles();
            ViewData["fullName"] = fullName;
            ViewData["street"] = street;
            ViewData["postalCode"] = postalCode;
            ViewData["city"] = city;
            ViewData["phoneNumber"] = phoneNumber;
            ViewData["deliveryOptions"] = deliveryOptions;
            ViewData["paymentOptions"] = paymentOptions;

            ClearCartCookies();

            return View("NewOrderConfirmation", cartArticlesWithQuantity);
        }

        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var articles = _context.Articles.Include(a => a.Category).ToList();
            return View(articles);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public IActionResult Index(string selectedCategoryValue)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            
            if (string.IsNullOrEmpty(selectedCategoryValue))
            {
                var articles = _context.Articles.Include(a => a.Category);
                return View(articles);
            }
            
            var articlesPart = _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == int.Parse(selectedCategoryValue));
            return View(articlesPart);
        }
        public void SetCookie(string key, string value, int? numberOfSeconds = WEEK)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }

        public string CreateArticleCoookieKey(int id)
        {
            return "article" + id.ToString();
        }
    }
}
