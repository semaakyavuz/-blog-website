using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _blog_website.Data;
using _blog_website.Models;
using System.Security.Claims;
using System.Linq;

namespace _blog_website.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // === YAZI OLUŞTURMA ===
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string content)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var post = new Post
            {
                Title = title,
                Content = content,
                AuthorEmail = userEmail,
                CreatedAt = DateTime.Now
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            TempData["Success"] = "Yazı başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        // === TÜM YAZILARI LİSTELEME ===
        [AllowAnonymous]
        public IActionResult Index()
        {
            var posts = _context.Posts
                                .OrderByDescending(p => p.CreatedAt)
                                .ToList();

            return View(posts);
        }

        // === YAZI DETAYI ===
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // === YAZI GÜNCELLEME ===
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(int id, string title, string content)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            post.Title = title;
            post.Content = content;
            _context.SaveChanges();

            TempData["Success"] = "Yazı başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            _context.Posts.Remove(post);
            _context.SaveChanges();

            TempData["Success"] = "Yazı başarıyla silindi.";
            return RedirectToAction("Index");
        }

    }
}
