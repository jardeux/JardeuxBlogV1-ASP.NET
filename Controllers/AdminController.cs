using System.Threading.Tasks;
using JardeuxBlogV1.Identity;
using JardeuxBlogV1.Models;
using JardeuxBlogV1.Models.ViewModels;
using JardeuxBlogV1.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JardeuxBlogV1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<BlogIdentityUser> _userManager;
        private readonly SignInManager<BlogIdentityUser> _signInManager;
        public AdminController(IUnitOfWork unitOfWork, UserManager<BlogIdentityUser> userManager, SignInManager<BlogIdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                LastPublishBlog = _unitOfWork.Blog.GetAll().OrderByDescending(u => u.PublishTime).FirstOrDefault(),
                MostViewBlog = _unitOfWork.Blog.GetAll().OrderByDescending(u => u.ViewCount).FirstOrDefault(),
                TotalBlogCount = _unitOfWork.Blog.GetAll().Count(),
                TotalViewCount = _unitOfWork.Blog.GetAll().Sum(u => u.ViewCount),
                TotalCommentCount = _unitOfWork.Comment.GetAll().Count(),
                MostCommentedBlog = _unitOfWork.Blog.GetAll().OrderByDescending(u => u.CommentCount).FirstOrDefault(),
                TodayCommentCount = _unitOfWork.Comment.GetAll(u => u.PublishDate.Date == DateTime.Now.Date).Count()

            };
            return View(dashboardViewModel);

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
            cekilenblog.ImageUrl = blog.ImageUrl;
            _unitOfWork.Blog.Update(cekilenblog);
            _unitOfWork.Save();
            TempData["success"] = "Başarıyla kaydedildi";
            return RedirectToAction("Index");
        }
        public IActionResult ChangeStatus(int id)
        {
            var changestatusobj = _unitOfWork.Blog.Get(u => u.Id == id);
            if (changestatusobj.Status == 0)
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
            var deletedobj = _unitOfWork.Blog.Get(u => u.Id == id);
            if (deletedobj == null)
            {
                return NotFound();
            }
            _unitOfWork.Blog.Remove(deletedobj);
            _unitOfWork.Save();
            TempData["success"] = "Başarıyla silindi";
            return RedirectToAction("Blogs");
        }
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(Blog blog)
        {
            blog.PublishTime = DateTime.Now;
            blog.Status = 1;
            _unitOfWork.Blog.Add(blog);
            _unitOfWork.Save();
            TempData["succes"] = "Yeni blog eklendi";
            return RedirectToAction("Blogs");
        }
        public IActionResult Comment(int? blogId)
        {
            var comments = new List<Comment>();
            if (blogId == null)
            {
                comments = _unitOfWork.Comment.GetAll().ToList();
            }
            else
            {
                comments = _unitOfWork.Comment.GetAll(u => u.BlogId == blogId).ToList();
            }



            return View(comments);
        }
        public IActionResult DeleteComment(int id)
        {
            var deletedid = _unitOfWork.Comment.Get(u => u.Id == id, includeProperties: "Blog");
            if (deletedid == null)
            {
                return NotFound();
            }
            _unitOfWork.Comment.Remove(deletedid);
            deletedid.Blog.CommentCount -= 1;
            _unitOfWork.Save();
            TempData["success"] = "Yorum başarıyla kaldırıldı";
            return RedirectToAction("Comment");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password == model.RePassword)
            {
                var user = new BlogIdentityUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                    TempData["success"] = "Kayıt başarılı";
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            TempData["success"] = "Çıkış başarılı";
            return RedirectToAction("Index", "Blogs");
        }
        public IActionResult Contact()
        {
            var contacts = _unitOfWork.Contact.GetAll();
            return View(contacts);
        }
        
        public IActionResult DeleteContact(int id)
        {
            var deletedid = _unitOfWork.Contact.Get(u => u.Id == id);
            _unitOfWork.Contact.Remove(deletedid);
            TempData["success"] = "İletişim isteği başarıyla silindi";
            _unitOfWork.Save();
            return RedirectToAction("Contact");
        }
    }
}
