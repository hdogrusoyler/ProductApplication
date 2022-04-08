using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DataAccess.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context;
        private IDbContextTransaction transaction;

        public IProductRepository productDal { get; set; }
        public IOrderRepository orderDal { get; set; }
        public IOrderProductRepository orderProductDal { get; set; }
        public IUserRepository userDal { get; set; }

        public EfUnitOfWork(DataContext _context, IProductRepository _productDal, IOrderRepository _orderDal, IOrderProductRepository _orderProductDal, IUserRepository _userDal)
        {
            context = _context;
            orderDal = _orderDal;
            productDal = _productDal;
            orderProductDal = _orderProductDal;
            userDal = _userDal;
        }
        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }
        public string CommitSaveChanges()
        {
            string result = "";
            try
            {
                result = Save().ToString();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                result = String.IsNullOrEmpty(e.InnerException?.Message) ? e.Message : e.InnerException.Message;
                //throw;
            }
            finally
            {
                transaction.Dispose();
                Dispose();
            }
            return result;
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
