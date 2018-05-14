using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.UserCodeReview;
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
        private readonly ICommentManager commentManager;
        private readonly ICodeManager codeManager;
        private readonly IMapper mapper;

        public ExerciseManagementController(IExerciseManager exerciseManager, ICourseManager courseManager,
                                            UserManager<User> userManager, ICodeManager codeManager , IMapper mapper, ICommentManager commentManager)
        {
            this.exerciseManager = exerciseManager;
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mapper = mapper;
            this.codeManager = codeManager;
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
            return View(new CreateExerciseViewModel()
            {
                CourseList = courseList
            });
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CreateExerciseViewModel model)
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (ModelState.IsValid)
            {
                var course = courseManager.GetById(model.CourseId);
                ExerciseDTO task = new ExerciseDTO
                {
                    CourseId = model.CourseId,
                    Course = course.Name,
                    TaskName = model.TaskName,
                    TaskTextField = model.TaskTextField,
                    TaskBaseCodeField = model.TaskBaseCodeField,
                    TestCasesCode = model.TestCases,
                    TeacherId = user.Id,
                    Rating = 0,
                    CreateDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.Now
                };
                exerciseManager.Insert(task);
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }


        public IActionResult TaskView(int id)
        {
            var task = exerciseManager.GetById(id);
            var commentList = commentManager.Get(g => g.ExerciseId == id).ToList();

            return View(new GetExerciseViewModel()
            {
                Id = id,
                Course = task.Course,
                CommentList = commentList,
                TaskName = task.TaskName,
                TaskTextField = task.TaskTextField,
                TaskBaseCodeField = task.TaskBaseCodeField
            });
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
                TaskBaseCodeField = task.TaskBaseCodeField,
                TestCases = task.TestCasesCode
            });

        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(UpdateExerciseViewModel model)
        {
            var course = courseManager.GetById(model.CourseId);
            if (ModelState.IsValid)
            {

                    exerciseManager.Update(model.Id, model.TaskName, model.TaskTextField,
                                       model.TaskBaseCodeField, model.CourseId,
                                       course.Name, DateTime.Now);
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteOrRecover(int id)
        {
            exerciseManager.DeleteOrRecover(id);
            return RedirectToAction("Index", "ExerciseManagement");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult ExerciseSolutionsIndex(int id)
        {
            List<UserCodeListUnitViewModel> codesList = new List<UserCodeListUnitViewModel>();
            var solutionsList = codeManager.Get(c => c.ExerciseId == id && c.CodeStatus == Model.Entity.CodeStatus.Done);
            if (solutionsList != null)
            {

                foreach (var elem in solutionsList)
                {
                    codesList.Add(new UserCodeListUnitViewModel
                    {
                        codeUnit = elem,
                        UserName = userManager.FindByIdAsync(elem.UserId).Result.UserName
                    });
                }

            }
            UserCodeListViewModel model = new UserCodeListViewModel { userCodeList = codesList };
            return View(model);
        }


        [HttpPost]
         [Authorize(Roles = "Teacher")]
        public IActionResult ExerciseSolutionsIndex(UserCodeListViewModel model)
        {
            var b = model.userCodeList;

            return RedirectToAction("ExerExerciseSolutionsIndex", "ExerciseManagement");

        }



    }
    }

