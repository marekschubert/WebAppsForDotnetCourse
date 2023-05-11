﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStudentWithIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() // for all
        {
            ViewData["Info"] = "AdminController -> For All";
            return View("Info");
        }

        [Authorize(Roles ="Dean")]
        public IActionResult ForDean() // for (Admin and Dean) role
        {
            ViewData["Info"] = "AdminController -> For (Admin and Dean)";
            return View("Info");
        }

        public IActionResult ForAdmin() // for Admin role
        {
            ViewData["Info"] = "AdminController -> For Admin";
            return View("Info");
        }

    }
}
