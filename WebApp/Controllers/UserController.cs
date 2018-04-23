using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.DTO;
using Remotion.Linq.Utilities;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<User> userManager, IMapper ucMapper, SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            mapper = ucMapper;
        }

        public   IActionResult Index()
        {
             var getuser = mapper.Map<UserDTO>(userManager.GetUserAsync(HttpContext.User).Result);           
           
            return View(getuser);

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = await userManager.FindByIdAsync(model.Id);
               // if (user != null)
               // {
                    IdentityResult result =
                        await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
               // else
               // {
                 //   ModelState.AddModelError(string.Empty, "User not found");
               // }
           // }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeUserName(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangeUserNameViewModel model = new ChangeUserNameViewModel { Id = user.Id, Email = user.Email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = await userManager.FindByIdAsync(model.Id);
                IdentityResult result =
                    await userManager.SetUserNameAsync(user,model.NewUserName) ;
            
               bool result2 = await userManager.CheckPasswordAsync(user, model.Password);
                if (result.Succeeded && result2)
                {                                
                    await  signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {                   
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                    if (!result2)
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect password");
                    }
                }
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEmail(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangeEmailViewModel model = new ChangeEmailViewModel { Id = user.Id, OldEmail = user.Email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                IdentityResult result = await userManager.SetEmailAsync(user, model.NewEmail);
                //  await  userManager.SetEmailAsync(user, model.NewEmail);
                //userManager.ChangeEmailAsync(user,model.OldEmail, model.NewEmail);

                bool result2 = await userManager.CheckPasswordAsync(user, model.Password);
                if (result.Succeeded && result2)
                {
                 //   await signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                    if (!result2)
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect password");
                    }
                }
            }
            return View(model);
        }


    }
}