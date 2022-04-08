using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Entity;

namespace Product.Application.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService userService;
        public IConfiguration Configuration { get; }
        public UserController(IUserService _userService, IConfiguration configuration)
        {
            userService = _userService;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }              

        //[AllowAnonymous]
        //[HttpGet]
        //public List<User> GetList()
        //{
        //    return userService.GetAll();
        //}

        //[HttpGet("{id}")]
        //public User Get(int Id)
        //{
        //    return userService.GetById(Id);
        //}

        [HttpPost]
        public string AddUpdate([FromBody] User title)
        {
            if (title.Id > 0)
            {
                return userService.Update(title);
            }
            else
            {
                return userService.Add(title);
            }
        }

        [HttpPost("{id}")]
        public string Delete(int Id)
        {
            return userService.Delete(new User { Id = Id });
        }
    }
}
