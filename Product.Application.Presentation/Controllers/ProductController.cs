using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Presentation.Models;
using System.Data;
using System.Text;

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

        [AllowAnonymous]
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

                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult AddProductList(IFormFile excelfile)
        {
            //var id = Int32.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
            if (excelfile != null && excelfile.Length > 0 && (Path.GetExtension(excelfile.FileName) == ".xls" || Path.GetExtension(excelfile.FileName) == ".xlsx"))
            {
                using (var fileStream = excelfile.OpenReadStream())
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var reader = ExcelReaderFactory.CreateReader(fileStream, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) }))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
                        var dataTable = result.Tables[0];

                        DataRow[] rows = dataTable.Rows.Cast<DataRow>().ToArray();

                        List<Entity.Product> products = new List<Entity.Product>(); 
                        
                        foreach (var datalist in rows.ToList())
                        {
                            var data = new Entity.Product();
                            try
                            {
                                data.ProductName = datalist[0].ToString();
                                data.Description = datalist[1].ToString();
                                data.Price = Convert.ToInt32(datalist[2]);
                                data.ProductQuantity = Convert.ToInt32(datalist[3]);
                                data.ProductCode = Guid.NewGuid().ToString();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            products.Add(data);
                        }
                        productService.AddProductList(products);
                    }
                }
            }

            return RedirectToAction("Index", "Product");
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
