using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICoverTypesRepository:IRepository<CoverType>
    {

        public void update( CoverType coverType);
    }
}
