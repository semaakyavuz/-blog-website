using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _blog_website.Models;
using _blog_website.Business.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _blog_website.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // === TÜM YAZILARI LİSTELEME ===
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        // === DETAY ===
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        // === OLUŞTURMA ===
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string content)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail)) return RedirectToAction("Login", "Account");

            var post = new Post
            {
                Title = title,
                Content = content,
                AuthorEmail = userEmail,
                CreatedAt = DateTime.Now
            };

            await _postService.CreatePostAsync(post);
            TempData["Success"] = "Yazı başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        // === GÜNCELLE ===
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title, string content)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            post.Title = title;
            post.Content = content;
            await _postService.UpdatePostAsync(post);

            TempData["Success"] = "Yazı başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        // === SİL ===
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _postService.DeletePostAsync(id, userEmail);

            TempData["Success"] = "Yazı başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
