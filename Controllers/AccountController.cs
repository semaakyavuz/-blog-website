using Microsoft.AspNetCore.Mvc;
using _blog_website.Data;
using _blog_website.Models;
using System.Linq;

namespace _blog_website.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                TempData["Success"] = "Giriş başarılı!";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "E-posta veya şifre hatalı.";
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string fullName, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                return View();
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                Username = email.Split('@')[0], // Kullanıcı adı otomatik atanıyor
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
