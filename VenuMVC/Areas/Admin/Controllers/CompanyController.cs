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
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        // GET: /<controller>/
        // GET: /<controller>/
        //inplace of ApplicationDbContect i replaced with IUnitOfWork and _db=_unitRepo
        //because i used repositories and implemented ApplicationDbContext in IUnitofWork
        //it means we are adding Repositories in the dependency Injection (services)
        
        private readonly IUnitOfWork _unitRepo;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitRepo = unitOfWork;
           
        }

        public IActionResult Index()
        {
            List<Company> objectOn = _unitRepo.Company.GetAll().ToList();

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
            

            if (Id == null || Id == 0)
            {//create
                return View(new Company());
            }
            else
            {
                //update
                Company companyobj = _unitRepo.Company.Get(u => u.id == Id);
                return View(companyobj);
            }

        }
        //multipartfuntionality in form
        [HttpPost]
        public IActionResult Upsert(Company companyobj)
        {

            /* if (CompanyVM.Company.Title == CompanyVM.Company.Price.ToString())
             {
                 ModelState.AddModelError("Name", "Name cannot be the same as Price");
             }
             else
             {

                 if (int.TryParse(CompanyVM.Company.Title, out _))
                 {
                     ModelState.AddModelError("Name", "Name cannot be a number");
                 }
             }*/
            if (ModelState.IsValid)
            {

                if (companyobj.id == 0)
                {
                    _unitRepo.Company.Add(companyobj);

                    TempData["sucess"] = "Company Data is Created Sucessfully";

                }
                else
                {
                    _unitRepo.Company.Update(companyobj);
                    TempData["sucess"] = "Company Data is Update Sucessfully";
                }
                _unitRepo.Save();
                return RedirectToAction("Index");

            }
            else
            {

                
                return View(companyobj);


            }
            



        }

        /* public IActionResult Edit(int? Id)
         {
             if (Id == null || Id == 0)
             {
                 return NotFound();
             }
             Company? CompanyFromDb = _unitRepo.Company.Get(u => u.id == Id);
             if (CompanyFromDb == null)
             {
                 return NotFound();
             }

             return View(CompanyFromDb);
         }
         [HttpPost]
         public IActionResult Edit(Company obj)
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
                 _unitRepo.Company.Update(obj);
                 _unitRepo.Save();
                 TempData["sucess"] = "Company Data is Updated Sucessfully";
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
            Company? CompanyFromDb = _unitRepo.Company.Get(u => u.id == Id); ;
            if (CompanyFromDb == null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Company? obj = _unitRepo.Company.Get(u => u.id == Id); ;
            if (obj == null)
            {
                return NotFound();
            }
            _unitRepo.Company.Remove(obj);
            _unitRepo.Save();
            TempData["sucess"] = "Company Data is Deleted Sucessfully";
            return RedirectToAction("Index");

        }*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objectOn = _unitRepo.Company.GetAll().ToList();
            return Json(new { data = objectOn });


        }
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var CompanyTobeDeleted = _unitRepo.Company.Get(u =>u.id==Id);
                if(CompanyTobeDeleted==null)
            {
                return Json(new { success = false, message = "Error  while deleting" });
                    
            }
           
                _unitRepo.Company.Remove(CompanyTobeDeleted);
                _unitRepo.Save();

            return Json(new { success = true, message="Delete successfull" });


        }
        #endregion
    }

}