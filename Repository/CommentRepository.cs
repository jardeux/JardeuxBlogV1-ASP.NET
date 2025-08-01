using JardeuxBlogV1.Data;
using JardeuxBlogV1.Models;
using JardeuxBlogV1.Repository.IRepository;

namespace JardeuxBlogV1.Repository
{
    public class CommentRepository : Repository<Comment> , ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }


    }
}
