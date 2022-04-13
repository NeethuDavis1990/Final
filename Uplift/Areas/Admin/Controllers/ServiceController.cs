
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.Viewmodels;


namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        //uploading images on the web server, to find out root path 
        private readonly IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public ServiceVM ServVM { get; set; }

        public ServiceController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
        }

        //GET
        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ServVM = new ServiceVM()
            {
                Service = new Models.Service(),
                CategoryList = _unitofwork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitofwork.Frequency.GetFrequencyListForDropDown(),
            };
            if (id != null)
            {
                ServVM.Service = _unitofwork.Service.Get(id.GetValueOrDefault());
            }

            return View(ServVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Service service)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        //Get
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Service service)
        {
            return View();
        }

        #region API CALLS

        public IActionResult Getall()
        {
            return Json(new { data = _unitofwork.Service.GetAll(IncludeProperties: "Category,Frequency") });
        }

        #endregion
    }
}
