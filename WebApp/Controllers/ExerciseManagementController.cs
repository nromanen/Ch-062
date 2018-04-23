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
using BAL.Interfaces;
using WebApp.ViewModels.CoursesViewModels;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    public class ExerciseManagementController : Controller
    {
        private readonly IExerciseManager exerciseManager;
        private readonly ICourseManager courseManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ExerciseManagementController(IExerciseManager exerciseManager,ICourseManager courseManager,
                                            UserManager<User> userManager, IMapper mapper)
        {
            this.exerciseManager = exerciseManager;
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            var exerciseList = exerciseManager.GetAll();
            return View(exerciseList);
        }

        
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            var courseList = courseManager.Get(x => x.IsActive).ToList();
            return View(courseList);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CreateExerciseViewModel model)
        {
            var currentTeacher = userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                var course  = courseManager.GetById(model.CourseId); 
                ExerciseDTO task = new ExerciseDTO { CourseId = model.CourseId, Course = course.Name,
                                               TaskName = model.TaskName, TaskTextField = model.TaskTextField, 
                                               TeacherId = currentTeacher.Result.Id, CreateDateTime = DateTime.Now,
                                               UpdateDateTime = DateTime.Now };
            }  
            return RedirectToAction("Index", "ExerciseManagement");
        }
        
        
        public IActionResult TaskView(int id)
        {
            var task = mapper.Map<ExerciseDTO>(exerciseManager.GetById(id));
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(int id)
        {
            var courseList = courseManager.Get(g => g.IsActive).ToList();
            var task = exerciseManager.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(new UpdateExerciseViewModel()
            {
                Id = id,
                CourseList = courseList,
                TaskName = task.TaskName,
                TaskTextField = task.TaskTextField,
            });
  
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(UpdateExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = courseManager.GetById(model.CourseId);
                var task = new ExerciseDTO()
                {
                    Id = model.Id,
                    TaskName = model.TaskName,
                    TaskTextField = model.TaskTextField,
                    CourseId = model.CourseId,
                    Course = course.Name,
                    UpdateDateTime = DateTime.Now
                };
                exerciseManager.Update(task);
            }                            
            return RedirectToAction("Index", "ExerciseManagement");
        }


        
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteOrRecover(int id)
        {
            var task = exerciseManager.GetById(id);
            if (task != null)
            {
                task.IsDeleted = !task.IsDeleted;
                exerciseManager.Update(task);
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }
    }
}