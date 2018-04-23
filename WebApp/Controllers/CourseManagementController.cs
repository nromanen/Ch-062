using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels.CoursesViewModels;
using BAL.Interfaces;
using AutoMapper;

namespace WebApp.Controllers
{
    public class CourseManagementController : Controller
    {
        private readonly ICourseManager courseManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CourseManagementController(ICourseManager courseManager, UserManager<User> userManager, IMapper mapper)
        {
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var coursesList = courseManager.GetAll();
            return View(coursesList);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CourseDTO model)
        {
            //TODO: Ask Roman
            var user = userManager.GetUserAsync(HttpContext.User);

            model.CreationDate = DateTime.Now;
            model.IsActive = true;
            model.UserId = user.Result.Id;
            if (ModelState.IsValid)
            {
                var course = new CourseDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    CreationDate = model.CreationDate,
                    UserId = model.UserId
                };
                courseManager.Insert(course);
            }
            return RedirectToAction("Index", "CourseManagement");
        }

        public IActionResult ViewCourses()
        {
            var currentTeacher = userManager.GetUserAsync(HttpContext.User);
            var courseList = courseManager.Get(course => course.UserId == currentTeacher.Result.Id);
            return View(courseList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editingCourse = courseManager.GetById(id);
            return View(editingCourse);
        }

        [HttpPost]
        public IActionResult Edit(CourseDTO course)
        {
            if (ModelState.IsValid)
            {
                var courseEdit = courseManager.GetById(course.Id);
                if (courseEdit != null)
                {
                    courseEdit.Name = course.Name;
                    courseEdit.Description = course.Description;
                    courseManager.Update(courseEdit);
                }
            }
            return RedirectToAction("Index", "CourseManagement");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Administrator")]
        public IActionResult Toggle(int id)
        {
            var course = courseManager.GetById(id);
            if (course != null)
            {
                course.IsActive = !course.IsActive;
                courseManager.Update(course);
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
            var teacherList = mapper.Map<List<UserDTO>>(userManager.GetUsersInRoleAsync("Teacher").Result);
            var course = courseManager.GetById(id);
            return View(new SuspendCourseViewModel()
            {
                TeacherList = teacherList,
                CourseId = course.Id,
                CourseName = course.Name
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeOwner(SuspendCourseViewModel model)
        {
            var course = courseManager.GetById(model.CourseId);
            if (course != null)
            {
                course.UserId = model.ResultTeacherId;
                courseManager.Update(course);
            }
            return RedirectToAction("Index", "CourseManagement");
        }

        //[HttpGet]
        //public IActionResult ShowExercise(int id)
        //{
        //    Need Exercisemanager to rewrite code
        //    var currentCourseId = uUnitOfWork.CourseRepo.GetById(id);
        //    var currentCourseName = currentCourseId.Name;
        //    var coursesList =
        //        mMapper.Map<List<ExerciseDTO>>(uUnitOfWork.ExerciseRepo.GetAll()
        //            .Where(x => x.Course == currentCourseName && !x.IsDeleted));
        //    return View(coursesList);
        //}

    }
}