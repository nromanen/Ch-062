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
using Model.DB.Code;
using Model.DTO.CodeDTO;

namespace WebApp.Controllers
{
    public class CodeController : Controller
    {
        private IExerciseManager exerciseManager;
        private IUnitOfWork unitOfWork;
        public CodeController(IExerciseManager exerciseManager, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.exerciseManager = exerciseManager;
        }
        public IActionResult Index(UserCodeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Home/Index");
            }

            var exercise = exerciseManager.GetById(model.ExerciseId);
            model.Exercise = exercise;
            model.CodeText = exercise.TaskBaseCodeField;
            var user = unitOfWork.UserRepo.Get(c => c.Email == model.UserId).First().Id;
            model.UserId = user;
            var findeCode = unitOfWork.CodeRepo.Get(c => c.UserId == user && c.ExerciseId == model.ExerciseId).Last();
            if (findeCode != null)
            {
                model.CodeText = findeCode.CodeText;
            }

            return View(model);
        }

        [HttpPost]
        public string GetCode(UserCodeDTO model)
        {
            UserCode code = new UserCode
            {
                CodeText = model.CodeText,
                UserId = model.UserId,
                ExerciseId = model.ExerciseId
            };
            unitOfWork.CodeRepo.Insert(code);
            unitOfWork.Save();
            return model.CodeText;
        }
    }
}