using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Entity;
using Product.Application.Presentation.Models;
using System.Security.Claims;

namespace Product.Application.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;
        public IConfiguration Configuration { get; }
        public AccountController(IUserService _userService, IConfiguration configuration)
        {
            userService = _userService;
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                userService.Add(new User() { UserName = model.UserName, Password = model.Password });
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            User user = new User();

            if (ModelState.IsValid)
            {
                user = userService.GetByUserName(model.UserName);
                if (user != null && user.Password == model.Password)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

                    return RedirectToAction("Index", "Product");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
