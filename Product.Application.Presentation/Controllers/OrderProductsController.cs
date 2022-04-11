using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.Entity;

namespace Product.Application.Presentation.Controllers
{
    [Authorize]
    public class OrderProductsController : Controller
    {
        private IOrderProductService orderProductService;
        public IConfiguration Configuration { get; }
        public OrderProductsController(IOrderProductService _orderProductService, IConfiguration configuration)
        {
            orderProductService = _orderProductService;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[AllowAnonymous]
        //[HttpGet]
        //public List<OrderProduct> GetList()
        //{
        //    return orderProductService.GetAll();
        //}

        //[HttpGet("{id}")]
        //public OrderProduct Get(int Id)
        //{
        //    return orderProductService.GetByOrderId(Id);
        //}

        [HttpPost]
        public string AddUpdate([FromBody] OrderProduct orderProduct)
        {
            return orderProductService.Add(orderProduct);
        }

        [HttpPost]
        public string Delete([FromBody] OrderProduct orderProduct)
        {
            return orderProductService.Delete(orderProduct);
        }

        [HttpPost("{id}")]
        public string Delete(int productId, int orderId)
        {
            return orderProductService.Delete(new OrderProduct { ProductId = productId, OrderId = orderId });
        }
    }
}
