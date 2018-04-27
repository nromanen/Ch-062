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

        public CodeHistoryController(ICodeManager codeManager, IMapper mapper, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.codeManager = codeManager;
            this.mapper = mapper;
        }

        public IActionResult History(CodeHistoryViewModel codeHistoryViewModel)
        {
            var userCode = codeManager.GetUserCodeById(userManager.Users.Where(e => e.UserName == User.Identity.Name).First().Id);
            
            codeHistoryViewModel.ErrorExecutionHistory = codeManager.GetCodeErrors(userCode.Id);
            return View();
        }
    }
}