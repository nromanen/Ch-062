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
using WebApp.ViewModels.CodeHistoryViewModel;

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

        public IActionResult History(CodeHistoryViewModel codeHistoryViewModel)
        {
            var userName = User.Identity.Name;
            var user = userManager.Users.Where(e => e.UserName == userName).FirstOrDefault();
            var code = codeManager.GetUserCodeById(user.Id);
            
            return View(code);
        }
    }
}