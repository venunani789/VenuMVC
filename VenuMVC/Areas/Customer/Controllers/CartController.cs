using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Venu.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Venu.DataAccess.Repository.IRepository;
using Venu.Models.ViewModels;
using Venu.Utilities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenuMVC.Areas.Customer.Controllers
{
    [Area("customer")]
    
    public class CartController : Controller
    {
        // GET: /<controller>/
        private readonly IUnitOfWork _unitRepo;
        [BindProperty]
        public ShopingCartVM ShopingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitRepo = unitOfWork;
        }
        public IActionResult Index()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;


            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShopingCartVM = new()
            {
                ShopingCartList = _unitRepo.ShopingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };
      
            foreach (var cart in ShopingCartVM.ShopingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShopingCartVM.OrderHeader.OrderTotal += (cart.Price*cart.Count);
            }
            return View(ShopingCartVM);
        }
        public IActionResult Summary() {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;


            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShopingCartVM = new()
            {
                ShopingCartList = _unitRepo.ShopingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };
            ShopingCartVM.OrderHeader.ApplicationUser = _unitRepo.ApplicationUser.Get(u => u.Id == userId);

            ShopingCartVM.OrderHeader.Name = ShopingCartVM.OrderHeader.ApplicationUser.Name;
            ShopingCartVM.OrderHeader.PhoneNumber = ShopingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShopingCartVM.OrderHeader.StreetAddress = ShopingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShopingCartVM.OrderHeader.City = ShopingCartVM.OrderHeader.ApplicationUser.City;
            ShopingCartVM.OrderHeader.State = ShopingCartVM.OrderHeader.ApplicationUser.State;
            ShopingCartVM.OrderHeader.PostalCode = ShopingCartVM.OrderHeader.ApplicationUser.Postalcode;
            foreach (var cart in ShopingCartVM.ShopingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShopingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }


            return View(ShopingCartVM);
                 }
        [HttpPost]

        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;


            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShopingCartVM.ShopingCartList = _unitRepo.ShopingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");
            ShopingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShopingCartVM.OrderHeader.ApplicationUserId = userId;

            // ShopingCartVM.OrderHeader.ApplicationUser = _unitRepo.ApplicationUser.Get(u => u.Id == userId);
           ApplicationUser applicationUser= _unitRepo.ApplicationUser.Get(u => u.Id == userId);

            foreach (var cart in ShopingCartVM.ShopingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShopingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //it is regular customer account and we need to capture payment
                ShopingCartVM.OrderHeader.PaymentStatus =SD.PaymentStatusPending;
                ShopingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
                //it is a company user
                ShopingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShopingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
            }
            try
            {
                _unitRepo.OrderHeader.Add(ShopingCartVM.OrderHeader);
                _unitRepo.Save();
                foreach (var cart in ShopingCartVM.ShopingCartList)
                {
                    OrderDetail orderDetail = new()
                    {


                        ProductId = cart.ProductId,
                        OrderHearderId = ShopingCartVM.OrderHeader.id,
                        Price = cart.Price,
                        Count = cart.Count

                    };

                    _unitRepo.OrderDetail.Add(orderDetail);
                    _unitRepo.Save();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //it is regular customer account and we need to capture payment
                //stripe logic
            }
            //return View(ShopingCartVM);
            return RedirectToAction(nameof(OrderConfirmation), new { id = ShopingCartVM.OrderHeader.id });
            
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

       
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitRepo.ShopingCart.Get(u => u.id == cartId);
            cartFromDb.Count += 1;
            _unitRepo.ShopingCart.Update(cartFromDb);
            _unitRepo.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitRepo.ShopingCart.Get(u => u.id == cartId);
            if (cartFromDb.Count <= 1)
            {
                _unitRepo.ShopingCart.Remove(cartFromDb);
            }
            else {
                cartFromDb.Count -= 1;
                _unitRepo.ShopingCart.Update(cartFromDb);
            }
           
            _unitRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitRepo.ShopingCart.Get(u => u.id == cartId);
            
                _unitRepo.ShopingCart.Remove(cartFromDb);
            
            
            _unitRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBasedOnQuantity(ShopingCart shopingCart)
        {
            if (shopingCart.Count <= 50)
            {
                return shopingCart.Product.Price;
            }
            else
            {
                if (shopingCart.Count <= 100)
                {
                    return shopingCart.Product.Price50;
                }
                else
                {
                    return shopingCart.Product.Price100;
                }
            }

       
        }
       
    }
}

