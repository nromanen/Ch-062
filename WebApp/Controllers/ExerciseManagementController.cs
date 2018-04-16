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
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    public class ExerciseManagementController : Controller
    {
        private IUnitOfWork uUnitOfWork;
        private IMapper mMapper;

        public ExerciseManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uUnitOfWork = unitOfWork;
            mMapper = mapper;
        }

      // [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            List<ExerciseDTO> t = new List<ExerciseDTO>();

            if (User.IsInRole("Teacher"))
            {
                t = mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.TaskRepo.GetAll());
            }
            else
            {
                t = mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.TaskRepo.GetAll().ToList().FindAll(x => !x.IsDeleted));
            }
          /*  int pageSize = 3;   

            var tests = uUnitOfWork.TaskRepo.GetAll();

            var count = tests.Count();
            var items = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            TaskListViewModel taskViewModel = new TaskListViewModel(count, page, pageSize);
            TaskListShowViewModel viewModel = new TaskListShowViewModel { Exercises = tests, ExerciseViewModel = taskViewModel };
            */
            return View(t);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            var t = uUnitOfWork.CourseRepo.GetAll().ToList().FindAll(x => x.IsActive);
            List<string> CourseList = new List<string>();
            CourseList.Add(null);
            foreach (var elem in t)
            {
                CourseList.Add(elem.Name);
            }
            // ViewBag.Courses = new SelectList(t, "Name");
            ViewBag.Courses = CourseList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CreateExerciseViewModel model)
        {
            var user = uUnitOfWork.UserRepo.GetAll().ToList().Find(c => c.Email == User.Identity.Name);


            //int id=1;
            if (ModelState.IsValid)
            {
                Exercise task = new Exercise { Course = model.Course, TaskName = model.TaskName, TaskString = model.TaskString, TeacherId = user.Id };

                try
                {
                    uUnitOfWork.TaskRepo.Insert(task);
                    uUnitOfWork.Save();
                    // id = task.Id;
                    // return RedirectToAction("Index", "TestManagement");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            
            // return View(mMapper.Map<ExerciseDTO>(uUnitOfWork.TaskRepo.GetById(id)));
            return RedirectToAction("Index", "ExerciseManagement");
        }

        public IActionResult TaskView(int id)
        {
            //  Exercise task =  uUnitOfWork.TaskRepo.GetById(id);
            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.TaskRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        [HttpPost]
        public  IActionResult TaskView()
        {
         return RedirectToAction("Index", "ExerciseManagement");
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Update(int id)
        {
            //  Exercise task =  uUnitOfWork.TaskRepo.GetById(id);
            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.TaskRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(UpdateExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Exercise task = uUnitOfWork.TaskRepo.GetById(model.Id);
                if (task != null)
                {
                    task.TaskName = model.TaskName;
                    task.TaskString = model.TaskString;
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
            //return View(mMapper.Map<ExerciseDTO>(uUnitOfWork.TaskRepo.GetById(model.Id));
            return RedirectToAction("Index", "ExerciseManagement");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(int id)
        {
            //var task = uUnitOfWork.TaskRepo.GetById(id);
            //try
            //{
            //    uUnitOfWork.TaskRepo.Delete(task);
            //    uUnitOfWork.Save();                       
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //}
            var task = uUnitOfWork.TaskRepo.GetById(id);
            if (task != null)
            {
                task.IsDeleted  = true;
                try
                {
                    uUnitOfWork.TaskRepo.Update(task);
                    uUnitOfWork.Save();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return RedirectToAction("Index", "ExerciseManagement");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Recover(int id)
        {
            //var task = uUnitOfWork.TaskRepo.GetById(id);
            //try
            //{
            //    uUnitOfWork.TaskRepo.Delete(task);
            //    uUnitOfWork.Save();                       
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //}
            var task = uUnitOfWork.TaskRepo.GetById(id);
            if (task != null)
            {
                task.IsDeleted = false;
                try
                {
                    uUnitOfWork.TaskRepo.Update(task);
                    uUnitOfWork.Save();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return RedirectToAction("Index", "ExerciseManagement");
        }

    }
}