using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitOfWork.category.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(category);
                _unitOfWork.save();
                TempData["success"] = "Category is added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }


        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.category.GetFirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.update(category);
                _unitOfWork.save();
                TempData["success"] = "Category is Editeded successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }


        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.category.GetFirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _unitOfWork.category.GetFirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.category.Remove(category);
            _unitOfWork.save();
            TempData["success"] = "Category is deleteded successfully";
            return RedirectToAction("Index");



        }

    }
}
