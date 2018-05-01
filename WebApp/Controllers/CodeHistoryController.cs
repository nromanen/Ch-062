using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interfaces;
using BAL.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.DTO.CodeDTO;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CodeHistoryController : Controller
    {
        private readonly ICodeManager codeManager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IExerciseManager exerciseManager;
        private readonly ICourseManager courseManager;

        public CodeHistoryController(ICodeManager codeManager, IMapper mapper, UserManager<User> userManager, IExerciseManager exerciseManager, ICourseManager courseManager)
        {
            this.userManager = userManager;
            this.codeManager = codeManager;
            this.mapper = mapper;
            this.exerciseManager = exerciseManager;
            this.courseManager = courseManager;
        }

        public IActionResult History()
        {
            var userName = User.Identity.Name;
            var user = userManager.Users.Where(e => e.UserName == userName).FirstOrDefault();
            var code = codeManager.GetUserCodeById(user.Id);
            var history = codeManager.GetHistoryLst(code.Id);
            var exercise = exerciseManager.Get().Where(e => e.Id == code.ExerciseId).FirstOrDefault();

            return View(new CodeHistoryViewModels {
                CodeHistory = history,
                ExerciseName = exercise.TaskName
            });
        }

        [HttpPost]
        public void AddToFavourites(int codeTextId, bool setToFavourite)
        {
            codeManager.SetFavouriteCode(codeTextId, setToFavourite);
        }
        [HttpGet]
        public string GetTaskName(int exerciseId)
        {
            var taskName = exerciseManager.Get().Where(e => e.Id == exerciseId).FirstOrDefault().TaskName;
            return taskName;
        }
    }
}