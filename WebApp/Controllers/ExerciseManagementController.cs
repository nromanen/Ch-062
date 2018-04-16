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

        ///Index///Index///Index///Index///Index///Index///Index///Index///Index///Index///
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

        ///Create///Create///Create///Create///Create///Create///Create///Create///Create///Create///
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            var c = uUnitOfWork.CourseRepo.GetAll().ToList().FindAll(x => x.IsActive);
            List<string> CourseList = new List<string>();
            CourseList.Add(null);
            foreach (var elem in c)
            {
                CourseList.Add(elem.Name);
            }
            ViewBag.Courses = CourseList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CreateExerciseViewModel model)
        {
            var user = uUnitOfWork.UserRepo.GetAll().ToList().Find(c => c.Email == User.Identity.Name);
            if (ModelState.IsValid)
            {
                Exercise task = new Exercise { Course = model.Course, TaskName = model.TaskName, TaskString = model.TaskString, TeacherId = user.Id, CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now };

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
        
        ///TaskView///TaskView///TaskView///TaskView///TaskView///TaskView///TaskView///TaskView///
        public IActionResult TaskView(int id)
        {
            //  Exercise task =  uUnitOfWork.ExerciseRepo.GetById(id);
            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.ExerciseRepo.GetById(id));
            if (t == null)
            {
                return NotFound();
            }
            return View(t);
        }

        ///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///UPDATE///
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(int id)
        {
            var c = uUnitOfWork.CourseRepo.GetAll().ToList().FindAll(x => x.IsActive);
            List<string> CourseList = new List<string>();
            CourseList.Add(null);
            foreach (var elem in c)
            {
                CourseList.Add(elem.Name);
            }
            ViewBag.Courses = CourseList;


            var t = mMapper.Map<ExerciseDTO>(uUnitOfWork.ExerciseRepo.GetById(id));
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
                Exercise task = uUnitOfWork.ExerciseRepo.GetById(model.Id);
                if (task != null)
                {
                    task.TaskName = model.TaskName;
                    task.TaskString = model.TaskString;
                    task.Course = model.Course;
                    task.UpdateDateTime = DateTime.Now;
                    try
                    {
                        uUnitOfWork.ExerciseRepo.Update(task);
                        uUnitOfWork.Save();
                        // return RedirectToAction("Index", "TestManagement");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }
            //return View(mMapper.Map<ExerciseDTO>(uUnitOfWork.ExerciseRepo.GetById(model.Id));
            return RedirectToAction("Index", "ExerciseManagement");
        }


        ///Delete///Delete///Delete///Delete///Delete///Delete///Delete///Delete///Delete///
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(int id)
        {
            //var task = uUnitOfWork.ExerciseRepo.GetById(id);
            //try
            //{
            //    uUnitOfWork.ExerciseRepo.Delete(task);
            //    uUnitOfWork.Save();                       
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //}
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

        ///Recover///Recover///Recover///Recover///Recover///Recover///Recover///Recover///
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Recover(int id)
        {
            //var task = uUnitOfWork.ExerciseRepo.GetById(id);
            //try
            //{
            //    uUnitOfWork.ExerciseRepo.Delete(task);
            //    uUnitOfWork.Save();                       
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //}
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