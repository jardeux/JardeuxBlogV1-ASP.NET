using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JardeuxBlogV1.Identity
{
    public class BlogIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
