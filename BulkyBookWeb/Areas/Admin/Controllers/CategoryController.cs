using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _unitOfWork.coverTypes.GetAll();
            return View(coverTypes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType covertype)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.coverTypes.Add(covertype);
                _unitOfWork.save();
                TempData["success"] = "covertype is added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(covertype);
            }


        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var covertype = _unitOfWork.coverTypes.GetFirstOrDefault(cover => cover.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType covertype)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.coverTypes.update(covertype);
                _unitOfWork.save();
                TempData["success"] = "covertype is Editeded successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(covertype);
            }


        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var covertype = _unitOfWork.coverTypes.GetFirstOrDefault(cover => cover.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var covertype = _unitOfWork.coverTypes.GetFirstOrDefault(cover => cover.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            _unitOfWork.coverTypes.Remove(covertype);
            _unitOfWork.save();
            TempData["success"] = "covertype is deleteded successfully";
            return RedirectToAction("Index");



        }

    }
}
