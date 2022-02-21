using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace CVProfile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Owner")]
    public class ExperienceController : Controller
    {
        private readonly IGenericRepository<WorkExperience> _experiencegenericRepository;
        public ExperienceController(IGenericRepository<WorkExperience> experiencegenericRepository)
        {
            _experiencegenericRepository = experiencegenericRepository;
        }
        public IActionResult Index()
        {
            return View(_experiencegenericRepository.GetAll());
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(WorkExperience workExperience)
        {
            var res = _experiencegenericRepository.Insertasync(workExperience).Result;
            if (res.Status == OperationResultStatus.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("Subject", res.Message);
            return PartialView(workExperience);
        }  
        
        public IActionResult Edit(int id)
        {
            return PartialView(_experiencegenericRepository.Getasync(id).Result);
        } 
        
        [HttpPost]
        public IActionResult Edit(WorkExperience workExperience)
        {
            var res = _experiencegenericRepository.Update(workExperience);
            if (res.Status == OperationResultStatus.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("Subject", res.Message);
            return PartialView(workExperience);
        }

        public IActionResult Delete(int id)
        {
            return PartialView(_experiencegenericRepository.Getasync(id).Result);
        } 
        
        [HttpPost]
        public IActionResult Delete(WorkExperience workExperience)
        {
           var res= _experiencegenericRepository.Delete(workExperience);
            if (res.Status==OperationResultStatus.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("Subject",res.Message);
            return PartialView(workExperience);
        }

    }
}
