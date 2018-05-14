using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using BAL.Managers;
using DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using WebApp.ViewModels;
using WebApp.ViewModels.UserCodeReview;
using Model.DB.Code;
using Model.DTO.CodeDTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class CodeController : Controller
    {
        private CodeManager codeManager;
        private IExerciseManager exerciseManager;

        public CodeController(CodeManager codeManager, IExerciseManager exerciseManager)
        {
            this.codeManager = codeManager;
            this.exerciseManager = exerciseManager;
        }
        public IActionResult Index(UserCodeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/Index");
            }
            return View(codeManager.BuildCodeModel(model));
        }

        [HttpPost]
        public string GetCode(UserCodeDTO model)
        {
            return model.CodeText == null ? "Write some code" : codeManager.ExecuteCode(model);
        }


        [HttpPost]
        public string ExecuteOnFlyCode(UserCodeDTO model)
        {
       
            return model.CodeText == null ? "Write some codeeeee" : codeManager.GetOnFlyCode(model);
        }


        [HttpPost]
        public IActionResult SetCodeStatus(UserCodeViewModel model)
        {
            var code = codeManager.UserCodeByExId(model.UserId, model.ExerciseId);
            codeManager.SetCodeStatus(code.Id);
            var IdForRedirect = new RedirectTempData();
            IdForRedirect.IdForRedirection = model.ExerciseId;
            //return RedirectToAction("TaskView", "ExerciseManagement", IdForRedirect.IdForRedirection);
            return View("../Home/Index");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CodeReview(int id)
        {
            var userCode = codeManager.Get(c => c.Id == id).First();
            var exercise = exerciseManager.GetById(userCode.ExerciseId);
            return View(new UserCodeReviewViewModel() {
                UserCodeDTO = userCode,
                ExerciseName = exercise.TaskName,
                ExerciseTask = exercise.TaskTextField
            });
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CodeMarking(UserCodeReviewViewModel model)
        {
            codeManager.SetMark(model.UserCodeDTO.Id, model.UserCodeDTO.Mark, model.UserCodeDTO.TeachersComment);
            //  return RedirectToAction("ExerciseSolutionsIndex","ExerciseManagement",model.UserCodeDTO.ExerciseId);
            return RedirectToAction("Index", "ExerciseManagement");
        }

    }
}