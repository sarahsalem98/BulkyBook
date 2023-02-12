using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public  interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        void update(OrderHeader orderHeader);
        void updateStatus(int id, string orderStatus,string? paymentStatus=null); 
    }
}
