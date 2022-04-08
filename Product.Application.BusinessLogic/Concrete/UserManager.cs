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
    public class UserManager : IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public User GetById(int Id)
        {
            User res = new User();
            res = unitOfWork.userDal.Get(c => c.Id == Id);
            return res;
        }

        public User GetByUserName(string Name)
        {
            User res = new User();
            res = unitOfWork.userDal.GetList(x => x.UserName == Name, (qry) => qry.OrderByDescending(x => x.Id), 1, 0).FirstOrDefault();
            return res;
        }

        public List<User> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<User> res = new List<User>();
            res = unitOfWork.userDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            return res;
        }

        public string Add(User entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.userDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }
        public string Update(User entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.userDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(User entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.userDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}
