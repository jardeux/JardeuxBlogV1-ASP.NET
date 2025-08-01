using JardeuxBlogV1.Models;
using JardeuxBlogV1.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace JardeuxBlogV1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var blogs = _unitOfWork.Blog.GetAll(u => u.Status == 1);
            return View(blogs);
        }
        public IActionResult Details(int id)
        {
            var blog = _unitOfWork.Blog.Get(u => u.Id == id);

            return View(blog);
        }
    }
}
