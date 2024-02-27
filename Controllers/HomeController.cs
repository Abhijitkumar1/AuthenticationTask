using AuthenticationTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace AuthenticationTask.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly StudentContext _context;

        public HomeController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Privacy()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Student()
        {
            var ShowData = _context.Students.ToList();
            return View(ShowData);
            
        }
        public IActionResult Admin()
        {
            return View();

        }
        public IActionResult GetData()
        {
            var ShowData = _context.Students.ToList();
            return Json(ShowData);
        }
        public IActionResult Create(Student model)
        {

            var blog = new Student()
            {
                Id = model.Id,
                Name = model.Name,
                Department = model.Department,
                Email = model.Email,

            };
            _context.Students.Add(blog);
            _context.SaveChanges();
            return Json(blog);


        }
        public IActionResult UpdateData(Student app)
        {
            var oldData = _context.Students.Find(app.Id);

            if (oldData != null)
            {

                oldData.Name = app.Name;
                oldData.Department = app.Department;
                oldData.Email = app.Email;
                _context.Students.Update(oldData);
                _context.SaveChanges();
                return new JsonResult("OK");
            }
            return new JsonResult("Not Found");

        }
        public IActionResult DeleteData(int Id)
        {
            var Data = _context.Students.Find(Id);

            _context.Students.Remove(Data);
            _context.SaveChanges();
            return new JsonResult("OK");
        }
    }
}
