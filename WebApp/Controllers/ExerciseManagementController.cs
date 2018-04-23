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
        private UserManager<User> userManager;

        public ExerciseManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uUnitOfWork = unitOfWork;
            mMapper = mapper;
        }

        
        public IActionResult Index()
        {
            List<ExerciseDTO> t = new List<ExerciseDTO>();

            if (User.IsInRole("Teacher"))
            {
                t = mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.ExerciseRepo.GetAll());
            }
            else
            {
                t = mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.ExerciseRepo.GetAll().ToList().FindAll(x => !x.IsDeleted));
            }
          
            return View(t);
        }

        
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            var t = mMapper.Map<List<CourseDTO>>(uUnitOfWork.CourseRepo.Get(x => x.IsActive).ToList());
            return View(t);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CreateExerciseViewModel model)
        {
             var user = uUnitOfWork.UserRepo.Get(c => c.Email == User.Identity.Name).First();
       //     var user = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var course  = uUnitOfWork.CourseRepo.GetById(model.CourseId); 
                Exercise task = new Exercise { CourseId = model.CourseId, Course = course.Name, TaskName = model.TaskName, TaskTextField = model.TaskTextField, TeacherId = user.Id, CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now };

                try
                {
                    uUnitOfWork.ExerciseRepo.Insert(task);
                    uUnitOfWork.Save();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }  
            return RedirectToAction("Index", "ExerciseManagement");
        }
        
        
        public IActionResult TaskView(int id)
        {
            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.ExerciseRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(int id)
        {


            var courseList = mMapper.Map<List<CourseDTO>>(uUnitOfWork.CourseRepo.Get(g => g.IsActive).ToList());
            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.ExerciseRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(new UpdateExerciseViewModel()
            {
                CourseList = courseList,
                TaskName = t.TaskName,
                TaskTextField = t.TaskTextField,
            });
  
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(UpdateExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Exercise task = uUnitOfWork.ExerciseRepo.GetById(model.Id);
                if (task != null)
                {
                    var course = uUnitOfWork.CourseRepo.GetById(model.CourseId);

                    task.TaskName = model.TaskName;
                    task.TaskTextField = model.TaskTextField;
                    task.CourseId = model.CourseId;
                    task.Course = course.Name;
                    task.UpdateDateTime = DateTime.Now;
                    try
                    {
                        uUnitOfWork.ExerciseRepo.Update(task);
                        uUnitOfWork.Save();                        
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }


        
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(int id)
        {
           
            var task = uUnitOfWork.ExerciseRepo.GetById(id);
            if (task != null)
            {
                task.IsDeleted  = true;
                try
                {
                    uUnitOfWork.ExerciseRepo.Update(task);
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
           
            var task = uUnitOfWork.ExerciseRepo.GetById(id);
            if (task != null)
            {
                task.IsDeleted = false;
                try
                {
                    uUnitOfWork.ExerciseRepo.Update(task);
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