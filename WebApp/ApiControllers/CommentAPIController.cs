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

namespace WebApp.ApiControllers
{
    [Produces("application/json")]
    [Route("api/CommentAPI")]
    public class CommentApiController : Controller
    {
        private readonly IExerciseManager exerciseManager;
        private readonly ICourseManager courseManager;
        private readonly UserManager<User> userManager;
        private readonly ICommentManager commentManager;
        private readonly ICodeManager codeManager;
        private readonly IMapper mapper;

        public CommentApiController(IExerciseManager exerciseManager, ICourseManager courseManager,
                                            UserManager<User> userManager, ICodeManager codeManager, IMapper mapper, ICommentManager commentManager)
        {
            this.exerciseManager = exerciseManager;
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mapper = mapper;
            this.codeManager = codeManager;
        }
        // GET: api/CommentAPI
        [HttpGet]
        public IEnumerable<CommentDTO> Get(int id)
        {
            return commentManager.Get(c=>c.Id==id).ToList();
        }

        // GET: api/CommentAPI/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/CommentAPI
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        

    }
}
