using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicsShopOnline.Domain.Entities;
using ComicsShopOnline.BusinessLogic.Data;
using ComicsShopOnline.Domain.Entities.User;
using ComicsShopOnline.Helpers.Hash;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ComicsShopOnline.BusinessLogic.Servieces
{
    public class UserService
    {
        private readonly AppDbContext _context = new AppDbContext();

        public bool Register(UserDBTable user, string plainPassword)
        {
            if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
                return false;

            user.Password = HashHelper.HashPassword(plainPassword);
            user.Role = 0;
            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var ve in error.ValidationErrors)
                    {
                        Debug.WriteLine($"Property: {ve.PropertyName} — Error: {ve.ErrorMessage}");
                    }
                }
                throw;
            }

        }

        public UserDBTable Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && HashHelper.VerifyPassword(password, user.Password))
                return user;

            return null;
        }

        public UserDBTable GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
