using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository category { get; private set; }
        public ICoverTypesRepository coverTypes{ get; private set; }
        public IProductRepository product { get; private set; }
        public ICompanyRepository company { get; private set; } 
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShopingCartRepository shopingCart { get; private set; }

        public IOrderDetailRepository orderDetail { get;private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork( ApplicationDbContext db )
        {
            _db=db;
            category =new CategoryRepository (_db);
            coverTypes =new CoverTypesRepository (_db);
            product = new ProductRepository(_db);
            company=new CompanyRepository(_db);
            ApplicationUser=new ApplicationUserRepository(_db);
            shopingCart = new ShopingCartRepository(_db);
            orderDetail = new OrderDetailRepository(_db);
            orderHeader=new OrderHeaderRepository(_db); 

        }
        

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
