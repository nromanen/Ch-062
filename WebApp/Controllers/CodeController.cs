using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.DB.Code;
using Model.DTO.CodeDTO;

namespace WebApp.Controllers
{
    public class CodeController : Controller
    {
        public IActionResult Index(UserCodeDTO model)
        {
            return View(model);
        }

        [HttpPost]
        public string GetCode(UserCodeDTO model)
        {
            var text = model.CodeText;
            return text;
        }
    }
}