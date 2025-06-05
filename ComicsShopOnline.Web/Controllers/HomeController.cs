using ComicsShopOnline.BusinessLogic.Data;
using ComicsShopOnline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicsShopOnline.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var comics = _context.Comics.ToList();
            var comicViewModels = Global.MapperInstance.Map<List<ComicViewModel>>(comics);
            return View(comicViewModels);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}