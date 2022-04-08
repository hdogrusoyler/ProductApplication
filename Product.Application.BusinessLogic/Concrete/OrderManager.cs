using Microsoft.EntityFrameworkCore;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.DataAccess;
using Product.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Concrete
{
    public class OrderManager : IOrderService
    {
        private IUnitOfWork unitOfWork;

        public OrderManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public Order GetById(int Id)
        {
            Order res = new Order();
            res = unitOfWork.orderDal.Get(c => c.Id == Id);
            return res;
        }

        public List<Order> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<Order> res = new List<Order>();
            res = unitOfWork.orderDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(c => c.OrderProducts).ThenInclude(p => p.Product));
            return res;
        }

        public string Add(Order entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }
        public string AddOrder(Order entity)
        {
            unitOfWork.BeginTransaction();
            var ord = unitOfWork.orderDal.Add(entity);
            foreach (var item in entity.OrderProducts)
            {
                item.Order = ord;
                unitOfWork.orderProductDal.Add(item);
                var prod = unitOfWork.productDal.Get(x => x.Id == item.ProductId);
                prod.ProductQuantity -= item.OrderProductQuantity;
                unitOfWork.productDal.Update(prod);
            }

            return unitOfWork.CommitSaveChanges();
        }
        public string Update(Order entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(Order entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}
