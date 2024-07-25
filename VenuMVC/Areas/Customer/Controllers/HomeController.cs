using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Venu.Models;
using Venu.DataAccess.Repository.IRepository;
using Venu.Models.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace VenuMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitRepo;
        
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitRepo = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitRepo.Product.GetAll(includeProperties: "Catogory");
            return View(ProductList);
        }
        public IActionResult Details(int productId)
        {
            ShopingCart cart = new()
            {
                Product = _unitRepo.Product.Get(u => u.id == productId, includeProperties: "Catogory"),
                Count = 1,
            ProductId = productId

            };
            
            return View(cart);
        }


       


    [HttpPost]

        
        public IActionResult Details(ShopingCart shopingCart)

        {
            if(User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
           

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shopingCart.ApplicationUserId = userId;
            ShopingCart cartFromDb = _unitRepo.ShopingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shopingCart.ProductId);
            if (cartFromDb != null)
            {  //shoping cart exists
                cartFromDb.Count += shopingCart.Count;
                _unitRepo.ShopingCart.Update(cartFromDb);
            }
            else
            {//add cart record
                _unitRepo.ShopingCart.Add(shopingCart);
            }
            TempData["sucess"] = "Cart Updated succesfully";
           
            _unitRepo.Save();

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

