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
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public FrequencyController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Frequency frequency = new Frequency();
            if (id == null)
            {
                return View(frequency);
            }
            frequency = _unitofwork.Frequency.Get(id.GetValueOrDefault());
            if (frequency == null)
            {
                return View(NotFound());
            }
            return View(frequency);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Frequency frequency)
        {
            
            if (ModelState.IsValid)
            {
                if (frequency.Id == 0)
                {
                    _unitofwork.Frequency.Add(frequency);
                }
                else
                {
                    _unitofwork.Frequency.Update(frequency);
                }
                _unitofwork.Save();
                return RedirectToAction("Index");

            }
            return View(frequency);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.Frequency.GetAll() });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ObjfromDb = _unitofwork.Frequency.Get(id);
            if (ObjfromDb == null)
            {
                return Json(new { success = "false", Message = "Error while deleting" });
            }
            _unitofwork.Frequency.Remove(ObjfromDb);
            _unitofwork.Save();
            return Json(new { success = "true", Message = "Delete successful " });
        }

        #endregion
    }
}
