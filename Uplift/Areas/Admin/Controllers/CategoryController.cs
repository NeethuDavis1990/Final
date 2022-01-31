using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
       
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data=_unitofwork.Category.GetAll()});
        }

        public IActionResult Delete(int id)
        {
            var ObjfromDb = _unitofwork.Category.Get(id);
            if(ObjfromDb==null)
            {
                return Json(new { success = "false", Message = "Error while deleting" });
            }
            _unitofwork.Category.Remove(ObjfromDb);
            return Json(new { success = "true", Message = "Delete successful " });
        }

        #endregion
    }
}
