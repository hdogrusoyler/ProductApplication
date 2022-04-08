using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Presentation.Models;

namespace Product.Application.Presentation.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProductService productService;
        public IConfiguration Configuration { get; }
        public ProductController(IProductService _productService, IConfiguration configuration)
        {
            productService = _productService;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            var result = productService.GetAll();
            return View(result);
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductModel product)
        {
            if (ModelState.IsValid)
            {
                var productModel = new Entity.Product()
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductCode = Guid.NewGuid().ToString(),
                    ProductName = product.ProductName,
                    ProductQuantity = product.ProductQuantity,
                };
                productService.Add(productModel);
            }
            return RedirectToAction("Index");
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public List<Entity.Product> GetList()
        //{
        //    return productService.GetAll();
        //}

        //[HttpGet("{id}")]
        //public Entity.Product Get(int Id)
        //{
        //    return productService.GetById(Id);
        //}

        [HttpPost]
        public string AddUpdate([FromBody] Entity.Product product)
        {
            if (product.Id > 0)
            {
                return productService.Update(product);
            }
            else
            {
                return productService.Add(product);
            }
        }

        [HttpPost("{id}")]
        public string Delete(int Id)
        {
            return productService.Delete(new Entity.Product { Id = Id });
        }
    }
}
