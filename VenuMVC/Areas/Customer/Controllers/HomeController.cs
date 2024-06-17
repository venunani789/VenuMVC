﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Venu.Models;
using Venu.DataAccess.Repository.IRepository;
using Venu.Models.Models;

namespace VenuMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties: "Catogory");
            return View(ProductList);
        }
        public IActionResult Details(int ProductId)
        {
            Product Product = _unitOfWork.Product.Get(u => u.id == ProductId, includeProperties: "Catogory");
            return View(Product);
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

