using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interfaces;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AddNewsController : Controller
    {
        private readonly ICourseManager courseManager;
        private readonly INewsManager newsManager;

        public AddNewsController(ICourseManager courseManager, INewsManager newsManager)
        {
            this.newsManager = newsManager;
            this.courseManager = courseManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult News()
        {
            try
            {
                var news = newsManager.GetAll().ToList();
                var courses = courseManager.GetAll().ToList();
                return View(new NewsViewModel()
                {
                    NewsDTO = news,
                    CoursesDTO = courses,
                    
                    
                });
            }
            catch(Exception ex)
            {

            }
            return View();
            
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile files, string Text, string Title, string Course)
        {
            // full path to file in temp location
            var filePath = "/images/" + files.FileName;
            var course = courseManager.Get().Where(e => e.Name == Course).FirstOrDefault();
           
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }
            NewsDTO newsDTO = new NewsDTO()
            {
                Text = Text,
                Title = Title ,
                ImagePath = filePath,
                Day = DateTime.Today.Day,
                Month = Enum.GetName(typeof(MonthEnum), 
                DateTime.Today.Month - 1),
                CourseId = course.Id
            };

            newsManager.Insert(newsDTO);

            var news = newsManager.GetAll().ToList();
            var courses = courseManager.GetAll().ToList();
            return View("News", new NewsViewModel()
            {
                NewsDTO = news,
                CoursesDTO = courses,
            });
            
        }
        public void AddArticle()
        {

            
        }

        public void DeleteOrRecoverArticle(NewsDTO newsDTO)
        {
            newsManager.DeleteOrRecover(newsDTO.Id);
        }

        public void UpdateArticle(NewsDTO newsDTO)
        {
            newsManager.Update(newsDTO);
        }
    }
}