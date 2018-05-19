using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ChangeUserDataViewModel
    {
        public string Id { get; set; }

        public string NewEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string NewUserName { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }





    }
}
