using System.Collections.Generic;
using System.Threading.Tasks;
using _blog_website.Models;

namespace _blog_website.Business.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id, string userEmail);
    }
}
