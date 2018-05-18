using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL;
using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.DTO;

namespace WebApp.Controllers
{
    public class EmailMessagesController : Controller
    {
        private readonly IMessagesManager messagesManager;
        private readonly IMapper mapper;

        public EmailMessagesController(IMessagesManager messagesManager, IMapper mapper)
        {
            this.messagesManager = messagesManager;
            this.mapper = mapper;
        }

        public IActionResult GetEmail(string Name, string Email, string Message)
        {
            messagesManager.Insert(new MessagesDTO() {FromEmail = Email });
            return View();
        }
        public IActionResult SendEmail(string email, string text, string subject)
        {
            EmailService emailService = new EmailService();
            emailService.SendEmail(email, text, subject);
            return View();
        }
    }
}