using AutoMapper;
using _blog_website.Models;
using _blog_website.Dtos;

namespace _blog_website.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
