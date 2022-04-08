using Product.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Abstract
{
    public interface IOrderService
    {
        Order GetById(int Id);
        List<Order> GetAll(int page = 1, int pageSize = 0);
        string Add(Order entity);
        string AddOrder(Order entity);
        string Update(Order entity);
        string Delete(Order entity);
    }
}
