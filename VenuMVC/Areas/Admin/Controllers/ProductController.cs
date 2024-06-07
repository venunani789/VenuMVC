using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Venu.Models.Models;
using Venu.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Venu.Utilities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenuMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        // GET: /<controller>/
        // GET: /<controller>/
        //inplace of ApplicationDbContect i replaced with IUnitOfWork and _db=_unitRepo
        //because i used repositories and implemented ApplicationDbContext in IUnitofWork
        //it means we are adding Repositories in the dependency Injection (services)
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitRepo;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitRepo = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objectOn = _unitRepo.Product.GetAll(includeProperties: "Catogory").ToList();

            return View(objectOn);
        }
        //public IActionResult Create()
        public IActionResult Upsert(int? Id)
        {
            //projections inEfCore
            /*   IEnumerable<SelectListItem> CatogoryList = _unitRepo.Catogory.GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.id.ToString()
               });*/
            //ViewBag.CatogoryList = CatogoryList;
            ProductVM productVM = new()
            {
                CatogoryList = _unitRepo.Catogory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString(),
                    //Value=u.DisplayOrder.ToString()
                }),
                //CatogoryList = CatogoryList,
                Product = new Product()

            };
            if (Id == null || Id == 0)
            {//create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitRepo.Product.Get(u => u.id == Id);
                return View(productVM);
            }

        }
        //multipartfuntionality in form
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            /* if (productVM.Product.Title == productVM.Product.Price.ToString())
             {
                 ModelState.AddModelError("Name", "Name cannot be the same as Price");
             }
             else
             {

                 if (int.TryParse(productVM.Product.Title, out _))
                 {
                     ModelState.AddModelError("Name", "Name cannot be a number");
                 }
             }*/

            if (ModelState.IsValid)
            {   //it will redirect to wwwrootFolder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {                         // file name and to preserve  extension of file 
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //to navigate to product path
                    string productPath = Path.Combine(wwwRootPath, @"images/product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {//delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    //to uploade the file into productPath
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    //to update file  url in the productVM
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVM.Product.id == 0)
                {
                    _unitRepo.Product.Add(productVM.Product);

                    TempData["sucess"] = "Product Data is Created Sucessfully";

                }
                else
                {
                    _unitRepo.Product.Update(productVM.Product);
                    TempData["sucess"] = "Product Data is Update Sucessfully";
                }
                _unitRepo.Save();
                return RedirectToAction("Index");

            }
            else
            {

                productVM.CatogoryList = _unitRepo.Catogory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString()
                });
                //CatogoryList = CatogoryList,
                return View(productVM);


            }
            



        }

        /* public IActionResult Edit(int? Id)
         {
             if (Id == null || Id == 0)
             {
                 return NotFound();
             }
             Product? ProductFromDb = _unitRepo.Product.Get(u => u.id == Id);
             if (ProductFromDb == null)
             {
                 return NotFound();
             }

             return View(ProductFromDb);
         }
         [HttpPost]
         public IActionResult Edit(Product obj)
         {
             if (obj.Title == obj.Price.ToString())
             {
                 ModelState.AddModelError("Name", "Name cannot be the same as Price");
             }
             else
             {

                 if (int.TryParse(obj.Title, out _))
                 {
                     ModelState.AddModelError("Name", "Name cannot be a number");
                 }
             }


             if (ModelState.IsValid)
             {
                 _unitRepo.Product.Update(obj);
                 _unitRepo.Save();
                 TempData["sucess"] = "Product Data is Updated Sucessfully";
                 return RedirectToAction("Index");
             }
             return View();
         }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitRepo.Product.Get(u => u.id == Id); ;
            if (ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Product? obj = _unitRepo.Product.Get(u => u.id == Id); ;
            if (obj == null)
            {
                return NotFound();
            }
            _unitRepo.Product.Remove(obj);
            _unitRepo.Save();
            TempData["sucess"] = "Product Data is Deleted Sucessfully";
            return RedirectToAction("Index");

        }*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objectOn = _unitRepo.Product.GetAll(includeProperties: "Catogory").ToList();
            return Json(new { data = objectOn });


        }
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var ProductTobeDeleted = _unitRepo.Product.Get(u =>u.id==Id);
                if(ProductTobeDeleted==null)
            {
                return Json(new { success = false, message = "Error  while deleting" });
                    
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, ProductTobeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
                _unitRepo.Product.Remove(ProductTobeDeleted);
                _unitRepo.Save();

            return Json(new { success = true, message="Delete successfull" });


        }
        #endregion
    }

}