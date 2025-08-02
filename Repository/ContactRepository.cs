using JardeuxBlogV1.Data;
using JardeuxBlogV1.Models;
using JardeuxBlogV1.Repository.IRepository;

namespace JardeuxBlogV1.Repository
{
    public class ContactRepository : Repository<Contact> ,IContactRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }


    }
}
