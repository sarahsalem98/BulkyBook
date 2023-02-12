using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
        [Area("Customer")]
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger ,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.product.GetAll(includeProperties:"Category,CoverType");
            return View(products);
        }


        public IActionResult Details(int  ProductId)
        {
            ShopingCart cart = new()
            {
                ProductId= ProductId,
                Product = _unitOfWork.product.GetFirstOrDefault(p => p.Id == ProductId, includeProperties: "Category,CoverType"),
                Count = 1

            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShopingCart shopingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shopingCart.ApplicationUserId = claim.Value;

            ShopingCart cartFromDb = _unitOfWork.shopingCart.GetFirstOrDefault(
                c => c.ApplicationUserId == claim.Value &&
                c.ProductId == shopingCart.ProductId
            );
            if (cartFromDb == null)
            {
            _unitOfWork.shopingCart.Add(shopingCart);

            }
            else
            {
                _unitOfWork.shopingCart.Increment(cartFromDb, shopingCart.Count);
            }

            _unitOfWork.save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}