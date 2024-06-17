using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Venu.DataAccess.Data;
using Venu.DataAccess.Repository.IRepository;
using Venu.DataAccess.Repository;
using Venu.Models.Models;
using Venu.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenuMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles=SD.Role_Admin)]
    public class CatogoryController : Controller
    {
        // GET: /<controller>/
        private readonly IUnitOfWork  _unitRepo;
        public CatogoryController(IUnitOfWork unitOfWork) {
            _unitRepo = unitOfWork;

        }
       
        public IActionResult Index()
        {
            List<Catogory> objectOn = _unitRepo.Catogory.GetAll().ToList();
            return View(objectOn);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Catogory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name cannot be the same as DisplayOrder");
            }
            else
            {

                if (int.TryParse(obj.Name, out _))
                {
                    ModelState.AddModelError("Name", "Name cannot be a number");
                }
            }

            if (ModelState.IsValid)
            {
               _unitRepo.Catogory.Add(obj);
                _unitRepo.Save();
                TempData["sucess"] = "Catogory Data is Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if(Id==null ||Id==0)
            {
                return NotFound();
                    }
            Catogory? catogoryFromDb = _unitRepo.Catogory.Get(u=>u.id==Id);
            if(catogoryFromDb==null)
            {
                return NotFound();
            }

            return View(catogoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Catogory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name cannot be the same as DisplayOrder");
            }
            else
            {

                if (int.TryParse(obj.Name, out _))
                {
                    ModelState.AddModelError("Name", "Name cannot be a number");
                }
            }


            if (ModelState.IsValid)
            {
                _unitRepo.Catogory.Update(obj);
                _unitRepo.Save();
                TempData["sucess"] = "Catogory Data is Updated Sucessfully";
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
            Catogory? catogoryFromDb = _unitRepo.Catogory.Get(u => u.id == Id); ;
            if (catogoryFromDb == null)
            {
                return NotFound();
            }

            return View(catogoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Catogory? obj = _unitRepo.Catogory.Get(u => u.id == Id); ;
            if(obj==null)
            {
                return NotFound();
            }
            _unitRepo.Catogory.Remove(obj);
            _unitRepo.Save();
            TempData["sucess"] = "Catogory Data is Deleted Sucessfully";
            return RedirectToAction("Index");
           
        }

    }
}

