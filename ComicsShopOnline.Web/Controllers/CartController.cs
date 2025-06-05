using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ComicsShopOnline.BusinessLogic.Data;
using ComicsShopOnline.BusinessLogic.Servieces;
using ComicsShopOnline.Domain.Entities.User;
using ComicsShopOnline.Domain.Entities;
using ComicsShopOnline.Web.Models;
using System.Data.Entity;
using ComicsShopOnline.Web.Profiles;

namespace ComicsShopOnline.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public CartController()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CartProfile());
            }).CreateMapper();

            _userService = new UserService();
            _context = new AppDbContext();
        }
        public CartController(IMapper mapper, AppDbContext context, UserService userService)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public UserDBTable GetCurrentUser()
        {
            if(!User.Identity.IsAuthenticated) return null;

            return _userService.GetUserByUsername(User.Identity.Name);
        }
        // GET: Cart
        public ActionResult Index()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
                return RedirectToAction("Login", "Account");

            var cartItems = _context.CartItems
                .Where(ci => ci.UserId == currentUser.Id)
                .Include(ci => ci.Comic)
                .ToList();

            var viewModel = new CartViewModel
            {
                Items = _mapper.Map<List<CartItemViewModel>>(cartItems)
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(int comicId)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null) return RedirectToAction("Login", "Account");

            var existingItem = _context.CartItems
                .FirstOrDefault(ci=>ci.UserId == currentUser.Id && ci.ComicId == comicId);

            if(existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                _context.CartItems.Add(new CartItemDBTable
                {
                    ComicId = comicId,
                    UserId = currentUser.Id,
                    Quantity = 1
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null)
            {
                // Проверяем, что товар принадлежит текущему пользователю
                var currentUser = GetCurrentUser();
                if (currentUser != null && item.UserId == currentUser.Id)
                {
                    _context.CartItems.Remove(item);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _context.CartItems.Find(id);
            if (item != null && quantity > 0)
            {
                // Проверяем, что товар принадлежит текущему пользователю
                var currentUser = GetCurrentUser();
                if (currentUser != null && item.UserId == currentUser.Id)
                {
                    item.Quantity = quantity;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}