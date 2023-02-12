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
    public class CoverTypesRepository : Repository<CoverType>, ICoverTypesRepository
    {
        private readonly ApplicationDbContext  _db;
        public CoverTypesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 

        }

        public void update(CoverType coverType)
        {
            _db.Update(coverType);
        }
    }
}
