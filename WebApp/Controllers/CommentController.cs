using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels.CommentViewModel;
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
    public class CommentController : Controller
    {
        private readonly ICommentManager commentManager;
        private readonly IExerciseManager exerciseManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CommentController(IExerciseManager exerciseManager, ICourseManager courseManager,
                                            UserManager<User> userManager, ICommentManager commentManager, IMapper mapper)
        {
            this.exerciseManager = exerciseManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mapper = mapper;
        }


        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(CreateCommentViewModel model)
        {
            var currentUser = userManager.GetUserAsync(HttpContext.User).Result.Id;

            if (ModelState.IsValid)
            {
                CommentDTO comment = new CommentDTO
                {
                ExerciseId = model.ExerciseId,
                UserId = currentUser,
                UserName = User.Identity.Name,
                CommentText = model.CommentText,
                Rating = model.Rating,
                CreationDateTime = DateTime.Now
                };
                commentManager.Insert(comment);
                if (model.Rating != null)
                {
                    var commentlist = commentManager.Get(g => g.ExerciseId == model.ExerciseId && g.Rating!=0).ToList();
                    double average = 0;
                    foreach (var elem in commentlist)
                    {
                        if(elem.Rating != 0)
                        average += Convert.ToDouble(elem.Rating);
                    }

                    average = average / commentlist.Count;
                    exerciseManager.UpdateRating(model.ExerciseId, average);

                }
            }
            return RedirectToAction("TaskView ", "ExerciseManagement", model.ExerciseId);
        }

    }
}