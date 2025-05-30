using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _blog_website.Data;
using _blog_website.Models;
using System.Linq;

namespace _blog_website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
