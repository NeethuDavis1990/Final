

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

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

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                return View(category);
            }
            category = _unitofwork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return View(NotFound());
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Category category)
        {
           
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitofwork.Category.Add(category);
                }
                else
                {
                    _unitofwork.Category.Update(category);
                }
                _unitofwork.Save();
                return RedirectToAction("Index");

            }
            return View(category);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.Category.GetAll() });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ObjfromDb = _unitofwork.Category.Get(id);
            if (ObjfromDb == null)
            {
                return Json(new { success = "false", Message = "Error while deleting" });
            }
            _unitofwork.Category.Remove(ObjfromDb);
            _unitofwork.Save();
            return Json(new { success = "true", Message = "Delete successful " });
        }

        #endregion
    }
}
