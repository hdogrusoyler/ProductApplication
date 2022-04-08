using Product.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Abstract
{
    public interface IOrderProductService
    {
        OrderProduct GetByOrderId(int Id);
        List<OrderProduct> GetAll(int page = 1, int pageSize = 0);
        string Add(OrderProduct entity);
        string Update(OrderProduct entity);
        string Delete(OrderProduct entity);
    }
}
