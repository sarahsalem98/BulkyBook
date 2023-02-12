using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public  interface IShopingCartRepository: IRepository<ShopingCart>
    {
        int Increment(ShopingCart shopingCart,int count); 
        int Decrement(ShopingCart shopingCart,int count); 

    }
}
