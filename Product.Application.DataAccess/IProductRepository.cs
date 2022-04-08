using Application.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DataAccess
{
    public interface IProductRepository : IBaseRepository<Entity.Product>
    {
    }
}
