using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext  _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 

        }

        public void update(Product product)
        {
            var productfromDB=_db.Products.FirstOrDefault(p => p.Id == product.Id);
            if(product != null)
            {
                productfromDB.Title = product.Title;
                productfromDB.ISBN = product.ISBN;
                productfromDB.Price = product.Price;
                productfromDB.ListPrice = product.ListPrice;
                productfromDB.Price100 = product.Price100;
                productfromDB.Price50 = product.Price50;
                productfromDB.Description = product.Description;
                productfromDB.Author = product.Author;
                productfromDB.CoverTypeId = product.CoverTypeId;
                productfromDB.CategoryId = product.CategoryId;
                if (product.ImageUrl != null)
                {
                    productfromDB.ImageUrl=product.ImageUrl;    
                }
            }
          
        }
    }
}
