using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {

            return View();
        }



        public IActionResult Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
                // return NotFo
                return View(company);
            }
            else
            {
                company = _unitOfWork.company.GetFirstOrDefault(p => p.Id == id);
                return View(company);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {


                if (company.Id == 0)
                {
                    _unitOfWork.company.Add(company);
                TempData["success"] = "company is added successfully";

                }
                else
                {
                    _unitOfWork.company.update(company);
                    TempData["success"] = "company is updated successfully";
                }
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(company);
            }


        }




        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.company.GetAll();
            return Json(new { data = companyList});
        }



        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            var company = _unitOfWork.company.GetFirstOrDefault(company => company.Id == id);
            if (company == null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
           
            _unitOfWork.company.Remove(company);
            _unitOfWork.save();
            return Json(new { success = true, message = "Deleting is successfull" });




        }
        #endregion

    }
}
