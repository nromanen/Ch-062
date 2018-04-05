using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Model.DB;

namespace WebApp.ViewModels
{
    public class ShowAllUsersViewModel
    {
        UnitOfWork UnitOfWork;
        public User thisUser { get; set; }
        IEnumerable<User> AllUsers { get; set; }
        public ShowAllUsersViewModel()
        {
            AllUsers = UnitOfWork.UserRepo.GetAll();
        }
         
        

    }
}
