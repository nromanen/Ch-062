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

      //  [Authorize(Roles = "Teacher")]
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


        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel model)
        {
            //int id=1;
            if (ModelState.IsValid)
            {
                TestTask task = new TestTask { Course = model.Course, TaskName = model.TaskName, TaskString=model.TaskString};

                try
                {
                    uUnitOfWork.TaskRepo.Insert(task);
                    uUnitOfWork.Save();
                   // id = task.ID;
                    // return RedirectToAction("Index", "TestManagement");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            // return View(mMapper.Map<TaskDTO>(uUnitOfWork.TaskRepo.GetById(id)));
            return RedirectToAction("Index", "TestManagement");
        }

        public IActionResult TaskView(int id)
        {
            //  TestTask task =  uUnitOfWork.TaskRepo.GetById(id);
            var t = mMapper.Map<TaskDTO>(uUnitOfWork.TaskRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        [HttpPost]
        public  IActionResult TaskView()
        {
         return RedirectToAction("Index", "TestManagement");
        }


        public IActionResult Update(int id)
        {
            //  TestTask task =  uUnitOfWork.TaskRepo.GetById(id);
            var t = mMapper.Map<TaskDTO>(uUnitOfWork.TaskRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        [HttpPost]
        public IActionResult Update(UpdateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                TestTask task = uUnitOfWork.TaskRepo.GetById(model.ID);
                if (task != null)
                {
                    task.TaskName = model.TaskName;
                    task.TaskString = String.Format(model.TaskString);
                    task.Course = model.Course;
                    try
                    {
                        uUnitOfWork.TaskRepo.Update(task);
                        uUnitOfWork.Save();
                        // return RedirectToAction("Index", "TestManagement");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }
            //return View(mMapper.Map<TaskDTO>(uUnitOfWork.TaskRepo.GetById(model.ID));
            return RedirectToAction("Index", "TestManagement");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var task = uUnitOfWork.TaskRepo.GetById(id);
                try
                {
                    uUnitOfWork.TaskRepo.Delete(task);
                    uUnitOfWork.Save();                       
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
        return RedirectToAction("Index", "TestManagement");
        }


    }
}