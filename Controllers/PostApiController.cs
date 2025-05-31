using Microsoft.AspNetCore.Mvc;
using _blog_website.Models;
using _blog_website.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using _blog_website.Dtos;
using AutoMapper;

namespace _blog_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostApiController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        // === TÜM POSTLARI GETİR ===
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return Ok(postDtos);
        }

        // === TEK POST GETİR ===
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
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
