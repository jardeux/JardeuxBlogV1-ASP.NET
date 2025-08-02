using JardeuxBlogV1.Models;
using Microsoft.EntityFrameworkCore;

namespace JardeuxBlogV1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        DbSet<Blog> Blogs { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Contact> Contacts { get; set; }



    }
}
