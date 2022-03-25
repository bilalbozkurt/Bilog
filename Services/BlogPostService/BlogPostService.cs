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
                var blogPosts = await _context.BlogPosts.Include(bp => bp.Categories).OrderByDescending(bp => bp.TimeCreated).ToListAsync();
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
                response.Success = false;
                response.Message = "Error";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<BlogPost>> GetSinglePostById(int id)
        {
            ServiceResponse<BlogPost> response = new ServiceResponse<BlogPost>();
            try
            {
                var blogPost = await _context.BlogPosts.Where(bp => bp.Id == id).Include(bp => bp.Categories).OrderByDescending(bp => bp.TimeCreated).FirstOrDefaultAsync();
                if (blogPost == null)
                {
                    response.Success = false;
                    response.Message = "No post found";
                    return response;
                }
                else
                {
                    response.Data = blogPost;
                }
            }
            catch (System.Exception)
            {
                response.Success = false;
                response.Message = "Error";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<BlogPost>> GetSinglePostByLink(string link)
        {
            ServiceResponse<BlogPost> response = new ServiceResponse<BlogPost>();
            try
            {
                var blogPost = await _context.BlogPosts.Where(bp => bp.Link == link).Include(bp => bp.Categories).OrderByDescending(bp => bp.TimeCreated).FirstOrDefaultAsync();
                if (blogPost == null)
                {
                    response.Success = false;
                    response.Message = "No post found";
                    return response;
                }
                else
                {
                    response.Data = blogPost;
                }
            }
            catch (System.Exception)
            {
                response.Success = false;
                response.Message = "Error";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<BlogPost>>> SearchHashtags(string searchInput)
        {
            ServiceResponse<List<BlogPost>> response = new ServiceResponse<List<BlogPost>>();
            try
            {
                searchInput = searchInput.Trim();

                if (searchInput.Length <= 0)
                {
                    response.Success = false;
                    response.Message = "No input provided.";
                    return response;
                }
                List<BlogPost> blogPosts = new List<BlogPost>();
                searchInput = "#" + searchInput;

                var category = await _context.Categories.Include(c => c.BlogPosts).Where(c => c.Name.ToLower() == searchInput.ToLower()).FirstOrDefaultAsync();
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "No hashtag found.";
                    return response;
                }

                if (category.BlogPosts == null)
                {
                    response.Success = false;
                    response.Message = "No post found.";
                    return response;
                }

                blogPosts = category.BlogPosts;
                if (blogPosts == null)
                {
                    response.Success = false;
                    response.Message = "No post found";
                    return response;
                }
                else
                {
                    var retPosts = new List<BlogPost>();
                    foreach (var item in blogPosts)
                    {
                        retPosts.Add(await _context.BlogPosts.Include(bp => bp.Categories).Where(bp => bp.Id == item.Id).FirstOrDefaultAsync()); //todo make here better
                    }
                    response.Data = retPosts;
                }
            }
            catch (System.Exception)
            {
                response.Success = false;
                response.Message = "Error";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<BlogPost>>> SearchPosts(string searchInput)
        {
            ServiceResponse<List<BlogPost>> response = new ServiceResponse<List<BlogPost>>();
            try
            {
                searchInput = searchInput.Trim();
                if (searchInput.Length <= 0)
                {
                    response.Success = false;
                    response.Message = "No input provided.";
                    return response;
                }
                List<BlogPost> blogPosts = new List<BlogPost>();

                blogPosts = await _context.BlogPosts.Where(bp => bp.Title.ToLower().IndexOf(searchInput.ToLower()) != -1).Include(bp => bp.Categories).OrderByDescending(bp => bp.TimeCreated).ToListAsync();
                if (blogPosts == null)
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
                response.Success = false;
                response.Message = "Error";
                return response;
            }

            return response;
        }
    }
}