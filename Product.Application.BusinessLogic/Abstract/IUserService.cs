using Product.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.BusinessLogic.Abstract
{
    public interface IUserService
    {
        User GetById(int Id);
        User GetByUserName(string Name);
        List<User> GetAll(int page = 1, int pageSize = 0);
        string Add(User entity);
        string Update(User entity);
        string Delete(User entity);
    }
}
