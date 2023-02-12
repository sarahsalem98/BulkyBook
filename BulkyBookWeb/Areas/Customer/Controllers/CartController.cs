using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModel;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BulkyBook.Models;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]

    public class CartController : Controller
    {
        private readonly IUnitOfWork _uniteOfWork;
        [BindProperty]
        public ShopingCartVM shopingCartVM { get; set; }

        public CartController(IUnitOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shopingCartVM = new ShopingCartVM()
            {
                ListCart = _uniteOfWork.shopingCart.GetAll(c => c.ApplicationUserId == claim.Value,includeProperties:"Product"),
                OrderHeader=new()
            };

            foreach(var cart in shopingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopingCartVM.OrderHeader.OrderTotal+= cart.Price * cart.Count;
            }
             return View(shopingCartVM); 
        }
        public IActionResult Summery()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shopingCartVM = new ShopingCartVM()
            {
                ListCart = _uniteOfWork.shopingCart.GetAll(c => c.ApplicationUserId == claim.Value, includeProperties: "Product"),
                OrderHeader = new()
            };
            shopingCartVM.OrderHeader.ApplicationUser = _uniteOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);
            shopingCartVM.OrderHeader.Name = shopingCartVM.OrderHeader.ApplicationUser.Email;
            shopingCartVM.OrderHeader.PhoneNumber = shopingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            shopingCartVM.OrderHeader.StreetAddress = shopingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            shopingCartVM.OrderHeader.City = shopingCartVM.OrderHeader.ApplicationUser.City;
            shopingCartVM.OrderHeader.State = shopingCartVM.OrderHeader.ApplicationUser.State;
            shopingCartVM.OrderHeader.PostalCode = shopingCartVM.OrderHeader.ApplicationUser.PostalCode;
            foreach (var cart in shopingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopingCartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }
            return View(shopingCartVM);

           
        }
        [HttpPost]
        [ActionName("Summury")]
        [AutoValidateAntiforgeryToken]
        public IActionResult SummeryPost()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shopingCartVM.ListCart = _uniteOfWork.shopingCart.GetAll
                (c => c.ApplicationUserId == claim.Value, includeProperties: "Product");
            shopingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            shopingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            shopingCartVM.OrderHeader.OrderDate =System.DateTime.Now;

            shopingCartVM.OrderHeader.ApplicationUser = _uniteOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);
            shopingCartVM.OrderHeader.Name = shopingCartVM.OrderHeader.ApplicationUser.Email;
            shopingCartVM.OrderHeader.PhoneNumber = shopingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            shopingCartVM.OrderHeader.StreetAddress = shopingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            shopingCartVM.OrderHeader.City = shopingCartVM.OrderHeader.ApplicationUser.City;
            shopingCartVM.OrderHeader.State = shopingCartVM.OrderHeader.ApplicationUser.State;
            shopingCartVM.OrderHeader.PostalCode = shopingCartVM.OrderHeader.ApplicationUser.PostalCode;
            foreach (var cart in shopingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopingCartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }
            _uniteOfWork.orderHeader.Add(shopingCartVM.OrderHeader);
            _uniteOfWork.save();

            foreach (var cart in shopingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = shopingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count

                };
                _uniteOfWork.orderDetail.Add(orderDetail);
                _uniteOfWork.save();
            }
            _uniteOfWork.shopingCart.RemoveRange(shopingCartVM.ListCart);
            _uniteOfWork.save();
            return RedirectToAction("Index","Home");


        }

        private double GetPriceBasedOnQuantity( double quentity,double price,double price50,double price100)
        {
            if (quentity <= 50)
            {
                return price;
            }
            else
            {
                if (quentity <= 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }

        }
        public IActionResult pluse(int cartId)
        {
            var cart = _uniteOfWork.shopingCart.GetFirstOrDefault(c => c.Id == cartId);
            _uniteOfWork.shopingCart.Increment(cart,1);
            _uniteOfWork.save();
            return RedirectToAction(nameof (Index));   
        }

        public IActionResult minus(int cartId)
        {
            var cart = _uniteOfWork.shopingCart.GetFirstOrDefault(c => c.Id == cartId);
            if (cart.Count <= 1)
            {
                _uniteOfWork.shopingCart.Remove(cart);
            }
            else
            {
            _uniteOfWork.shopingCart.Decrement(cart, 1);

            }
            _uniteOfWork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult remove(int cartId)
        {
            var cart = _uniteOfWork.shopingCart.GetFirstOrDefault(c => c.Id == cartId);
            _uniteOfWork.shopingCart.Remove(cart);
            _uniteOfWork.save();
            return RedirectToAction(nameof(Index));
        }
    }
}

