using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using BAL.Managers;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using WebApp.ViewModels;
using Model.DB.Code;
using Model.DTO.CodeDTO;

namespace WebApp.Controllers
{
    [Authorize]
    public class CodeController : Controller
    {
        private CodeManager codeManager;

        public CodeController(CodeManager codeManager)
        {
            this.codeManager = codeManager;
        }
        public IActionResult Index(UserCodeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/Index");
            }
            return View(model);
        }

        [HttpPost]
        public string GetCode(UserCodeDTO model)
        {
            return model.CodeText == null ? "Write some code" : codeManager.ExecuteCode(model);
        }


        [HttpPost]
        public string ExecuteCode(UserCodeDTO model)
        {
            return model.CodeText == null ? "Write some codeeeee" : GetCode(model);
        }


        /*  [HttpPost]
          public ActionResult SetCodeStatus(UserCodeViewModel model)
          {
              var code = codeManager.UserCodeByExId(model.UserId, model.ExerciseId);
              codeManager.SetCodeStatus(code.Id);
              return RedirectToAction("TaskView ", "ExerciseManagement", model.ExerciseId);
          }
          */


        //public ActionResult CodeReview(int )
        //{

        //}
    }
}