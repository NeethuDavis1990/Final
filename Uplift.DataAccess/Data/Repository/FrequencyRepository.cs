using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _db;
        public FrequencyRepository(ApplicationDbContext db):base (db)
        {
            _db = db;
        }

     

        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {

           return _db.Frequency.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }) ;
        }

     

        public void Update(Frequency frequency)
        {
            var objfromDb = _db.Frequency.FirstOrDefault(X => X.Id == frequency.Id);
            objfromDb.Name = frequency.Name;
            objfromDb.FrequencyCount = frequency.FrequencyCount;

            _db.SaveChanges();

        }

    }
}
