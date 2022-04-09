using Microsoft.EntityFrameworkCore;
using Product.Application.BusinessLogic.Abstract;
using Product.Application.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Concrete
{
    public class ProductManager : IProductService
    {
        private IUnitOfWork unitOfWork;

        public ProductManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public Entity.Product GetById(int Id)
        {
            Entity.Product res = new Entity.Product();
            res = unitOfWork.productDal.Get(c => c.Id == Id);
            return res;
        }

        public List<Entity.Product> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<Entity.Product> res = new List<Entity.Product>();
            res = unitOfWork.productDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(c => c.OrderProducts).ThenInclude(o => o.Order));
            return res;
        }
        public string AddProductList(List<Entity.Product> entityList)
        {
            unitOfWork.BeginTransaction();
            foreach (var item in entityList)
			{                
                unitOfWork.productDal.Add(item);                
            }
            return unitOfWork.CommitSaveChanges();
        }

        public string Add(Entity.Product entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.productDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }
        public string Update(Entity.Product entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.productDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(Entity.Product entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.productDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}
