using System.Linq;
using BAL.Interfaces;
using BAL.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using WebApp.ViewModels;
using WebApp.ViewModels.UserCodeReview;
using Model.DTO.CodeDTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class CodeController : Controller
    {
        private CodeManager codeManager;
        private IExerciseManager exerciseManager;
        private UserManager<User> userManager;

        public CodeController(CodeManager codeManager, IExerciseManager exerciseManager, UserManager<User> userManager)
        {
            this.codeManager = codeManager;
            this.exerciseManager = exerciseManager;
            this.userManager = userManager; 
        }
        public IActionResult Index(UserCodeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/Index");
            }
            var exercise = exerciseManager.GetById(model.ExerciseId);
            return View(new CodeStartViewModel() {
                UserCodeDTO = codeManager.BuildCodeModel(model),
                ExerciseTaskText = exercise.TaskTextField
        });
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
            if (code != null)
            {
                codeManager.SetCodeStatus(code.Id, code.UserId);
                var IdForRedirect = new RedirectTempData();
                IdForRedirect.IdForRedirection = model.ExerciseId;
            }
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
            codeManager.SetMark(model.UserCodeDTO.Id, model.UserCodeDTO.Mark, model.UserCodeDTO.TeachersComment,model.UserCodeDTO.UserId);
            //  return RedirectToAction("ExerciseSolutionsIndex","ExerciseManagement",model.UserCodeDTO.ExerciseId);
            return RedirectToAction("Index", "ExerciseManagement");
        }

    }
}