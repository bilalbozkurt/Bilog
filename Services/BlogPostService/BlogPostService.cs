using bilog.Data;
using bilog.Models;
using Microsoft.EntityFrameworkCore;

namespace bilog.Services.BlogPostService
{
    public class BlogPostService : IBlogPostService
    {
        private readonly DataContext _context;
        public BlogPostService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<BlogPost>>> GetAllPosts()
        {
            ServiceResponse<List<BlogPost>> response = new ServiceResponse<List<BlogPost>>();
            try
            {
                var blogPosts = await _context.BlogPosts.ToListAsync();
                if (blogPosts.Count <= 0)
                {
                    response.Success = false;
                    response.Message = "No post found";
                    return response;
                }
                else
                {
                    response.Data = blogPosts;
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return response;
        }

        public async Task<ServiceResponse<BlogPost>> GetSinglePostById(int id)
        {
            ServiceResponse<BlogPost> response = new ServiceResponse<BlogPost>();
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }

            return response;
        }
    }
}