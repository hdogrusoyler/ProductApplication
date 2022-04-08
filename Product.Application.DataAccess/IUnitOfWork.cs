using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DataAccess
{
    public interface IUnitOfWork
    {
        IProductRepository productDal { get; set; }
        IOrderRepository orderDal { get; set; }
        IOrderProductRepository orderProductDal { get; set; }
        IUserRepository userDal { get; set; }

        void BeginTransaction();
        string CommitSaveChanges();
        int Save();
        void Dispose();
    }
}
