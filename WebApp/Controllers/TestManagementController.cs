using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;
using Model.DB;
using Model.DTO;
using Microsoft.AspNetCore.Authorization;
using DAL.Interface;
using AutoMapper;

namespace WebApp.Controllers
{
    public class TestManagementController : Controller
    {
        private IUnitOfWork uUnitOfWork;
        private IMapper mMapper;
        public TestManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uUnitOfWork = unitOfWork;
            mMapper = mapper;
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            var t = mMapper.Map<List<TaskDTO>>(uUnitOfWork.TaskRepo.GetAll());
          /*  int pageSize = 3;   

            var tests = uUnitOfWork.TaskRepo.GetAll();

            var count = tests.Count();
            var items = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            TaskListViewModel taskViewModel = new TaskListViewModel(count, page, pageSize);
            TaskListShowViewModel viewModel = new TaskListShowViewModel { testTasks = tests, TaskViewModel = taskViewModel };
            */
            return View(t);
        }

        
        public IActionResult TaskView(int id)
        {
            TestTask task = uUnitOfWork.TaskRepo.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            GetTaskViewModel model = new GetTaskViewModel { TaskName = task.TaskName, TaskString = task.TaskString };
            return View(model);
            
        }


    }
}