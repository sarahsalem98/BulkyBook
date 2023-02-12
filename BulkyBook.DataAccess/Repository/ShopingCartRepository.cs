using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ShopingCartRepository : Repository<ShopingCart>, IShopingCartRepository
    {

        private readonly ApplicationDbContext _db;
       
        public ShopingCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
            
        }

        public int Decrement(ShopingCart shopingCart, int count)
        {
           shopingCart.Count -= count;
            return shopingCart.Count;
        }

        public int Increment(ShopingCart shopingCart, int count)
        {
            shopingCart.Count += count;
            return shopingCart.Count;
        }
    }
}
