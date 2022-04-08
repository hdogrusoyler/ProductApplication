namespace Product.Application.Presentation.Models
{
    public class LoginModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AddProductModel
    {
        //public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
    }

    public class AddOrderModel
    {
        public AddOrderModel()
        {
            if (ProductList == null)
            {
                ProductList = new List<AddOrderProductModel>();
            }
        }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public List<AddOrderProductModel> ProductList { get; set; }
    }

    public class AddOrderProductModel
    {
        public Entity.Product AddProduct { get; set; } = new Entity.Product();
        public int AddOrderProductQuantity { get; set; }
    }
}
