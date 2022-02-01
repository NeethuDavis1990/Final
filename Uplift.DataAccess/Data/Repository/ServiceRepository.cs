using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository :Repository<Service>,IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

       

        public void Update(Service service)
        {
            var objfromDb = _db.Service.FirstOrDefault(x => x.Id == service.Id);
            objfromDb.Name = service.Name;
            objfromDb.Price = service.Price;
            objfromDb.LongDesc = service.LongDesc;
            objfromDb.ImageUrl = service.ImageUrl;
            objfromDb.FrequencyId = service.FrequencyId;
            objfromDb.CategoryId = service.CategoryId;
            _db.SaveChanges();
        }
    }
}
