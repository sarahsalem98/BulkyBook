using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository category { get; }
        public ICoverTypesRepository coverTypes { get; }
        public IProductRepository product { get; }
        public ICompanyRepository company { get; }
        public IApplicationUserRepository ApplicationUser { get; }  

        public IShopingCartRepository shopingCart { get; }  
        public IOrderDetailRepository orderDetail { get; }
        public IOrderHeaderRepository orderHeader { get; }

        public void save();
    }
}
