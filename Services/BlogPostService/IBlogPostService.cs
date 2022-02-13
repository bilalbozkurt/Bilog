using bilog.Models;

namespace bilog.Services.BlogPostService
{
    public interface IBlogPostService
    {
         Task<ServiceResponse<BlogPost>> GetSinglePostById(int id);
         Task<ServiceResponse<List<BlogPost>>> GetAllPosts();
    }
}