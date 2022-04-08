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
    public class OrderProductManager : IOrderProductService
    {
        private IUnitOfWork unitOfWork;

        public OrderProductManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public OrderProduct GetByOrderId(int Id)
        {
            OrderProduct res = new OrderProduct();
            res = unitOfWork.orderProductDal.GetList(c => c.OrderId == Id, (qry)=>qry.OrderBy(x => x.OrderId), 1, 0, i=>i.Include(o => o.Order).Include(p => p.Product)).FirstOrDefault();
            return res;
        }

        public List<OrderProduct> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<OrderProduct> res = new List<OrderProduct>();
            res = unitOfWork.orderProductDal.GetList(null, (qry) => qry.OrderByDescending(x => x.OrderId), page, pageSize, i => i.Include(c => c.Order).Include(p => p.Product));//i => i.Photos
            return res;
        }

        public string Add(OrderProduct entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderProductDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Update(OrderProduct entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderProductDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(OrderProduct entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.orderProductDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}
