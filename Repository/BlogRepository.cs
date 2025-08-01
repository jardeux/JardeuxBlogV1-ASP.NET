using JardeuxBlogV1.Data;
using JardeuxBlogV1.Models;
using JardeuxBlogV1.Repository.IRepository;

namespace JardeuxBlogV1.Repository
{
    public class BlogRepository : Repository<Blog> ,IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }


    }
}
