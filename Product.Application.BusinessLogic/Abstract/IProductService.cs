using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Abstract
{
    public interface IProductService
    {
        Entity.Product GetById(int Id);
        List<Entity.Product> GetAll(int page = 1, int pageSize = 0);
        string AddProductList(List<Entity.Product> entityList);
        string Add(Entity.Product entity);
        string Update(Entity.Product entity);
        string Delete(Entity.Product entity);
    }
}
