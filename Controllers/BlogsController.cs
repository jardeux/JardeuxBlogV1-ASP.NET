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
            var comments = _unitOfWork.Comment.GetAll(u => u.BlogId == id);
            ViewBag.Comments = comments.ToList();
            return View(blog);
        }
        [HttpPost]
        public IActionResult CreateComment(Comment model)
        {
            model.PublishDate = DateTime.Now;
            _unitOfWork.Comment.Add(model);
            _unitOfWork.Save();
            return RedirectToAction("Details", new {id = model.BlogId});
        }
    }
}
