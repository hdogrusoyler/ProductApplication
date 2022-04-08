using Application.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DataAccess.EntityFramework
{
    public class EfProductRepository : EfBaseRepository<Entity.Product, DataContext>, IProductRepository
    {
        public EfProductRepository(DataContext dbContext) : base(dbContext)
        {

        }
    }
}
