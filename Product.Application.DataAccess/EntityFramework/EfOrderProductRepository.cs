using Application.Core.DataAccess.EntityFramework;
using Product.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DataAccess.EntityFramework
{
    public class EfOrderProductRepository : EfBaseRepository<OrderProduct, DataContext>, IOrderProductRepository
    {
        public EfOrderProductRepository(DataContext dbContext) : base(dbContext)
        {

        }
    }
}
