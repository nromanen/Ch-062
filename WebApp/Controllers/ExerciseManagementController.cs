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
        private readonly ICommentManager commentManager;
        private readonly IMapper mapper;
        private readonly ITestCaseManager testCaseManager;

        public ExerciseManagementController(IExerciseManager exerciseManager, ICourseManager courseManager,
                                            UserManager<User> userManager, IMapper mapper, ITestCaseManager testCaseManager, ICommentManager commentManager)
        {
            this.exerciseManager = exerciseManager;
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mapper = mapper;
            this.testCaseManager = testCaseManager;
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
                TaskBaseCodeField = task.TaskBaseCodeField
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
        public IActionResult Testcases(int id)
        {
            var testcases = testCaseManager.GetByExerciseId(id);
            return View(testcases);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult EditTestCase(int id)
        {
            var testcase = testCaseManager.GetById(id);
            return View(testcase);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult EditTestCase(TestCaseDTO testCaseDTO)
        {
            if (ModelState.IsValid)
            {
                testCaseManager.Update(testCaseDTO);
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateTestCase()
        {
            return View(new CreateTestCase() { ExerciseList = exerciseManager.GetAll() });
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateTestCase(CreateTestCase model)
        {
            model.TestCaseDTO.UserId = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var test = new TestCaseDTO()
                {
                    ExerciseId = model.TestCaseDTO.ExerciseId,
                    UserId = model.TestCaseDTO.UserId,
                    InputData = model.TestCaseDTO.InputData,
                    OutputData = model.TestCaseDTO.OutputData
                };
                testCaseManager.Insert(test);
            }
            return RedirectToAction("Index", "ExerciseManagement");
        }
    }
}
