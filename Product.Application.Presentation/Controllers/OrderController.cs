using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Entity;
using Product.Application.Presentation.Models;

namespace Product.Application.Presentation.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private IProductService productService;
        public IConfiguration Configuration { get; }
        public OrderController(IOrderService _orderService, IProductService _productService, IConfiguration configuration)
        {
            orderService = _orderService;
            productService = _productService;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            var result = orderService.GetAll();
            return View(result);
        }
        public IActionResult AddOrder()
        {
            AddOrderModel model = new AddOrderModel();

            #region TempData
            if (TempData["ProductList"] is string s)
            {
                AddOrderModel prodModel = JsonConvert.DeserializeObject<AddOrderModel>(s);
                model = prodModel;
                TempData.Remove("ProductList");
            }
            #endregion

            #region ProductList
            var productList = productService.GetAll();
            var productListItem = new List<SelectListItem>();
            foreach (var product in productList)
            {
                productListItem.Add(new SelectListItem() { Text = product.ProductCode + " " + product.ProductName, Value = product.Id.ToString() });
            }
            ViewBag.ProductList = productListItem;
            #endregion

            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToOrder(AddOrderModel model)
        {
            if (ModelState.IsValid)
            {
                var pro = model.ProductList.Where(x => x.AddProduct.Id == model.ProductId).FirstOrDefault();
                if (pro != null)
                {
                    pro.AddOrderProductQuantity += model.ProductQuantity;
                }
                else
                {
                    var prod = productService.GetById(model.ProductId);
                    model.ProductList.Add(new AddOrderProductModel() { AddProduct = prod, AddOrderProductQuantity = model.ProductQuantity });
                }

                TempData["ProductList"] = JsonConvert.SerializeObject(model);

                return RedirectToAction("AddOrder");
            }

            #region ProductList
            var productList = productService.GetAll();
            var productListItem = new List<SelectListItem>();
            foreach (var product in productList)
            {
                productListItem.Add(new SelectListItem() { Text = product.ProductCode + " " + product.ProductName, Value = product.Id.ToString() });
            }
            ViewBag.ProductList = productListItem;
            #endregion

            return View("AddOrder", model);
        }

        [HttpPost]
        public IActionResult AddOrder(AddOrderModel model)
        {
            //if (ModelState.IsValid)
            if (model.ProductList.Count > 0)
            {
                var order = new Order()
                {
                    Date = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString()
                    //OrderNumber = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)
                };
                foreach (var item in model.ProductList)
                {
                    order.OrderProducts.Add(new OrderProduct() { ProductId = item.AddProduct.Id, OrderProductQuantity = item.AddOrderProductQuantity });
                }
                orderService.AddOrder(order);

                return RedirectToAction("Index");
            }

            #region AddModelError
            ModelState.AddModelError("ProductList", "Please Add Prduct!");
			#endregion

			#region ProductList
			var productList = productService.GetAll();
            var productListItem = new List<SelectListItem>();
            foreach (var product in productList)
            {
                productListItem.Add(new SelectListItem() { Text = product.ProductCode + " " + product.ProductName, Value = product.Id.ToString() });
            }
            ViewBag.ProductList = productListItem;
            #endregion

            return View(model);
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public List<Order> GetList()
        //{
        //    return orderService.GetAll();
        //}

        //[HttpGet("{id}")]
        //public Order Get(int Id)
        //{
        //    return orderService.GetById(Id);
        //}

        [HttpPost]
        public string AddUpdate([FromBody] Order order)
        {
            if (order.Id > 0)
            {
                return orderService.Update(order);
            }
            else
            {
                return orderService.Add(order);
            }
        }

        [HttpPost("{id}")]
        public string Delete(int Id)
        {
            return orderService.Delete(new Order { Id = Id });
        }
    }
}
