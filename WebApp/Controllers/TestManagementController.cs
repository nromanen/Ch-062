using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;
using Model.DB;
using Microsoft.AspNetCore.Authorization;
using DAL.Interface;

namespace WebApp.Controllers
{
    public class TestManagementController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public TestManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "teacher")]
        public IActionResult Index(int page = 1)
        {
            int pageSize = 3;   // количество элементов на странице

            var tests = _unitOfWork.TaskRepo.GetAll();

            var count = tests.Count();
            var items = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            TaskListViewModel taskViewModel = new TaskListViewModel(count, page, pageSize);
            TaskListShowViewModel viewModel = new TaskListShowViewModel { testTasks = tests, TaskViewModel = taskViewModel };

            return View(viewModel);
        }
    }
}