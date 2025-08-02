using JardeuxBlogV1.Models;
using JardeuxBlogV1.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace JardeuxBlogV1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blogs()
        {
            var blogs = _unitOfWork.Blog.GetAll();
            return View(blogs);
        }
        public IActionResult EditBlog(int id)
        {
            var cekilenblog = _unitOfWork.Blog.Get(u => u.Id == id);
            return View(cekilenblog);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            var cekilenblog = _unitOfWork.Blog.Get(u => u.Id == blog.Id);

            if (cekilenblog == null)
            {
                return NotFound();
            }
            cekilenblog.Name = blog.Name;
            cekilenblog.Description = blog.Description;
            cekilenblog.Tags = blog.Tags;
            _unitOfWork.Blog.Update(cekilenblog);
            _unitOfWork.Save();
            TempData["success"] = "Başarıyla kaydedildi";
            return RedirectToAction("Index");
        }
        public IActionResult ChangeStatus(int id)
        {
            var changestatusobj = _unitOfWork.Blog.Get(u => u.Id == id);
            if(changestatusobj.Status == 0)
            {
                changestatusobj.Status = 1;
            }
            else
            {
                changestatusobj.Status = 0;
            }
            _unitOfWork.Save();
            TempData["success"] = "Başarıyla görünüm değiştirildi";
            return RedirectToAction("Blogs");
        }
        
        public IActionResult Delete(int id)
        {
            var deletedobj = _unitOfWork.Blog.Get(u => u.Id == id, includeProperties:"Comment");
            if (deletedobj == null)
            {
                return NotFound();
            }
            _unitOfWork.Blog.Remove(deletedobj);
            _unitOfWork.Save();
            TempData["success"] = "Başarıyla silindi";
            return RedirectToAction("Blogs");
        }
    }
}
