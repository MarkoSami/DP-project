using Giftify.DAL.Repository.Interfaces;
using Giftify.Models.ViewModels;
using Giftify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Giftify.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Giftify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.ADMIN)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> Companys = unitOfWork.Company.GetAll();
            return View("Index", Companys);
        }
        public IActionResult Upsert(int? id)
        {
            Company model = new Company();
         
            if (id == null || id == 0)
            {
                return View(model);
            }
            else
            {
                model = unitOfWork.Company.Get((u) => u.Id == id);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    unitOfWork.Company.Update(model);
                }
                else
                {
                    unitOfWork.Company.Add(model);
                }
                unitOfWork.Save();
                TempData["success"] = "Company Created successfully";
                return RedirectToAction("Index");
            }
            else
            {
            return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            Company CompanyToDelete = unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToDelete == null) return NotFound();

            unitOfWork.Company.Remove(CompanyToDelete);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }



        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> Companys = unitOfWork.Company.GetAll();
            return Json(new { data = Companys });
        }
        #endregion
    }
}

