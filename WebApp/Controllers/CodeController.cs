using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DB;

namespace WebApp.Controllers
{
    public class CodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetCode(UserCode model)
        {
            var text = model.CodeText;
            return PartialView(text);
        }
    }
}