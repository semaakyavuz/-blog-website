using Microsoft.AspNetCore.Mvc;
using _blog_website.Models;
using _blog_website.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;

namespace _blog_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostApiController(IPostService postService)
        {
            _postService = postService;
        }

        // === TÜM POSTLARI GETİR ===
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        // === TEK POST GETİR ===
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // === POST EKLE ===
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            post.AuthorEmail = userEmail;
            post.CreatedAt = DateTime.Now;

            await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        // === POST GÜNCELLE ===
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Post updatedPost)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.AuthorEmail != User.FindFirstValue(ClaimTypes.Email))
                return Unauthorized();

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            await _postService.UpdatePostAsync(post);

            return NoContent();
        }

        // === POST SİL ===
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _postService.DeletePostAsync(id, userEmail);

            return NoContent();
        }
    }
}
