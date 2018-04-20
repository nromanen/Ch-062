using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.DB;
using DAL.Interface;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.ViewModels.CoursesViewModels;

namespace WebApp.Controllers
{
    public class CourseManagementController : Controller
    {

        private readonly IUnitOfWork uUnitOfWork;
        private readonly IMapper mMapper;
        private readonly UserManager<User> userManager;


        public CourseManagementController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            uUnitOfWork = unitOfWork;
            mMapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var coursesList = mMapper.Map<List<CourseDTO>>(uUnitOfWork.CourseRepo.GetAll());
            return View(coursesList);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CourseDTO model)
        {
            var user = uUnitOfWork.UserRepo.GetAll().ToList().Find(c => c.Email == User.Identity.Name);
            model.CreationDate = DateTime.Now;
            model.IsActive = true;
            model.UserId = user.Id;
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    CreationDate = model.CreationDate,
                    UserId = model.UserId
                };

                uUnitOfWork.CourseRepo.Insert(course);
                uUnitOfWork.Save();
            }

            return RedirectToAction("Index", "CourseManagement");
        }

        public IActionResult ViewCourses()
        {
            var currentTeacherId = uUnitOfWork.UserRepo.GetAll().First(x => x.UserName == User.Identity.Name).Id;
            var coursesList =
                mMapper.Map<List<CourseDTO>>(uUnitOfWork.CourseRepo.GetAll().Where(x => x.UserId == currentTeacherId));
            return View(coursesList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editingCourse = uUnitOfWork.CourseRepo.GetById(id);
            var course = mMapper.Map<CourseDTO>(editingCourse);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(CourseDTO course)
        {
            if (ModelState.IsValid)
            {
                var courseEdit = uUnitOfWork.CourseRepo.GetById(course.Id);
                if (courseEdit != null)
                {
                    courseEdit.Name = course.Name;
                    courseEdit.Description = course.Description;
                    uUnitOfWork.CourseRepo.Update(courseEdit);
                    uUnitOfWork.Save();
                }
            }

            return RedirectToAction("Index", "CourseManagement");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Administrator")]
        public IActionResult Toggle(int id)
        {
            var course = uUnitOfWork.CourseRepo.GetById(id);
            if (course != null)
            {
                course.IsActive = !course.IsActive;
                uUnitOfWork.CourseRepo.Update(course);
                uUnitOfWork.Save();
            }

            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("ViewCourses", "CourseManagement");
            }

            return RedirectToAction("Index", "CourseManagement");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeOwner(int id)
        {
            var teacherList = mMapper.Map<List<UserDTO>>(userManager.GetUsersInRoleAsync("Teacher").Result);
            var course = mMapper.Map<CourseDTO>(uUnitOfWork.CourseRepo.GetById(id));
            return View(new SuspendCourseViewModel()
            {
                TeacherList = teacherList,
                CourseId = course.Id,
                CourseName = course.Name
            });
        }

        [HttpPost]
        public IActionResult ChangeOwner(SuspendCourseViewModel model)
        {
            var course = uUnitOfWork.CourseRepo.GetById(model.CourseId);
            course.UserId = model.ResultTeacherId;
            uUnitOfWork.CourseRepo.Update(course);
            uUnitOfWork.Save();
            return RedirectToAction("Index", "CourseManagement");
        }

        [HttpGet]
        public IActionResult ShowExercise(int id)
        {
            var currentCourseId = uUnitOfWork.CourseRepo.GetById(id);
            var currentCourseName = currentCourseId.Name;  
            var coursesList =
                mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.ExerciseRepo.GetAll()
                    .Where(x => x.Course == currentCourseName && !x.IsDeleted));


            return View(coursesList);
        }

    }
}