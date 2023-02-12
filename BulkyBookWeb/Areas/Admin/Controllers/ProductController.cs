using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            
            return View();
        }

       

        public IActionResult Upsert(int? id )
        {
            ProductVM productVM = new()
            {
                Product = new (),
                CategoryList = _unitOfWork.category.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                CoverTypeList = _unitOfWork.coverTypes.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
            };
            if (id == null || id == 0)
            {
                // return NotFo
                return View(productVM);
             }
            else
            {
                  productVM.Product=_unitOfWork.product.GetFirstOrDefault(p=>p.Id==id);
                   return View(productVM);
            }
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM ,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extention = Path.GetExtension(file.FileName);
                    if (productVM.Product.ImageUrl != null)
                    {
                        var oldImagePath=Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStreams=new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + fileName + extention;
                }
                if (productVM.Product.Id == 0)
                {
                _unitOfWork.product.Add(productVM.Product);

                }
                else
                {
                    _unitOfWork.product.update(productVM.Product);
                }
                _unitOfWork.save();
                TempData["success"] = "product is added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(productVM);
            }


        }
   



        #region API
        [HttpGet]
        public IActionResult GetAllProduts()
        {
            var productlist = _unitOfWork.product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productlist });
        }



        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.product.GetFirstOrDefault(product => product.Id == id);
            if (product == null)
            {
                return Json(new { success=false, message="Error while deleteing" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.product.Remove(product);
            _unitOfWork.save();
            return Json(new { success = true, message = "Deleting is successfull" });
            



        }
        #endregion

    }
}
