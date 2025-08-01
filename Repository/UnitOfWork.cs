using JardeuxBlogV1.Data;
using JardeuxBlogV1.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JardeuxBlogV1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        
        public IBlogRepository Blog { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Blog = new BlogRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
