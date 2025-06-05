using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicsShopOnline.BusinessLogic.Data;
using ComicsShopOnline.Web.Filters;
using ComicsShopOnline.Web.Models;

namespace ComicsShopOnline.Web.Controllers
{
    [AdminMode]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users); // afisarea utilizatorilor
        }
    }
}