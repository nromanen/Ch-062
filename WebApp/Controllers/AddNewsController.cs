using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interfaces;
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

        public IActionResult Create()
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
                    CoursesDTO = courses
                });
            }
            catch(Exception ex)
            {

            }
            return View();
            
        }

        public void AddArticle(NewsDTO newsDTO)
        {
            newsManager.Insert(newsDTO);
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