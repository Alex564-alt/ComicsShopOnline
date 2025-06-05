using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using ComicsShopOnline.BusinessLogic.Servieces;
using ComicsShopOnline.Domain.Entities.User;
using ComicsShopOnline.Domain.Enums;
using ComicsShopOnline.Web.Models;

namespace ComicsShopOnline.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService = new UserService();

        //GET Views/Account/Register.cshtml
        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Global.MapperInstance.Map<UserDBTable>(model);
                user.CreationDate = DateTime.Now;  // Устанавливаем дату

                try
                {
                    bool success = _userService.Register(user, model.Password);
                    if (success)
                    {
                        var authTicket = new FormsAuthenticationTicket(
                        version: 1,
                        name: user.Username,
                        issueDate: DateTime.Now,
                        expiration: DateTime.Now.AddMinutes(30),
                        isPersistent: false,
                        userData: user.Role.ToString() // Сохраняем роль!
                        );

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(authCookie);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "User already exists.");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }

            // Выводим ошибки валидации
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Debug.WriteLine($"{error.ErrorMessage}");
            }

            return View(model);
        }


        //GET: Views/Account/Login.cshtml
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userService.Authenticate(username, password);
            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(
                    version: 1,
                    name: username,
                    issueDate: DateTime.Now,
                    expiration: DateTime.Now.AddMinutes(30),
                    isPersistent: false,
                    userData: user.Role.ToString() // Сохраняем роль!
                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            // Удалим cookie вручную (перестраховка)
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = ""
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}