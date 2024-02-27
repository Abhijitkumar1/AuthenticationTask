using AuthenticationTask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationTask.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated) 
                return RedirectToAction("Student", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login modelLogin)
        {
            if (modelLogin.Email == "kumarabhijit018@gmail.com" &&
                modelLogin.Password == "12345"
               )
            {
                List<Claim> claims = new List<Claim>();
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email);
                    new Claim("otherProperties", "Example Role");
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                return RedirectToAction("Student", "Home");
            }
            ViewData["ValidateMessage"] = "User Not Found";
            return View();
        }



    }
}
