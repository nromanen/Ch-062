using System.Linq;
using Microsoft.AspNetCore.Identity;
using Model;
using DAL.Interface;
using Model.DB;
using Model.DB.Code;
using System.Collections.Generic;

namespace DAL.Seed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MainDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUnitOfWork unitOfWork;

        public DbInitializer(
            MainDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
        }

        public void Initialize()
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
                string userAdmin = "admin@gmail.com";
                string passwordAdmin = "Admin_123";
                userManager.CreateAsync(new User { UserName = userAdmin, Email = userAdmin, EmailConfirmed = true }, passwordAdmin).Wait();
                var t = userManager.FindByNameAsync(userAdmin);
                userManager.AddToRoleAsync(t.Result, "Administrator").Wait();
            }

            if (!context.Roles.Any(r => r.Name == "Teacher"))
            {
                roleManager.CreateAsync(new IdentityRole("Teacher")).Wait();
                string userTeacher = "teacher@gmail.com";
                string passwordTeacher = "Teacher_123";
                userManager.CreateAsync(new User { UserName = userTeacher, Email = userTeacher, EmailConfirmed = true }, passwordTeacher).Wait();
                var t = userManager.FindByNameAsync(userTeacher);
                userManager.AddToRoleAsync(t.Result, "Teacher").Wait();
            }
            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                roleManager.CreateAsync(new IdentityRole("Student")).Wait();
                string userStudent = "student@gmail.com";
                string passwordStudent = "Student_123";
                userManager.CreateAsync(new User { UserName = userStudent, Email = userStudent, EmailConfirmed = true }, passwordStudent).Wait();
                var t = userManager.FindByNameAsync(userStudent);
                userManager.AddToRoleAsync(t.Result, "Student").Wait();
            }
            if (!context.Courses.Any(r => r.Name == ".Net"))
            {
                unitOfWork.CourseRepo.Insert(new Course
                {
                    Name = ".Net",
                    Description = "Courses for .Net group",
                    IsActive = true,
                    CreationDate = System.DateTime.Now,
                    UserId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
            }
            if (!context.Courses.Any(r => r.Name == "Java"))
            {
                unitOfWork.CourseRepo.Insert(new Course
                {
                    Name = "Java",
                    Description = "Courses for Java group",
                    IsActive = true,
                    CreationDate = System.DateTime.Now,
                    UserId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
            }
            if (!context.Courses.Any(r => r.Name == "JavaScript"))
            {
                unitOfWork.CourseRepo.Insert(new Course
                {
                    Name = "JavaScript",
                    Description = "Courses for JavaScript group",
                    IsActive = true,
                    CreationDate = System.DateTime.Now,
                    UserId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
                unitOfWork.Save();
            }
            if (!context.Exercises.Any(r => r.TaskName == ".Net Task 1"))
            {
                unitOfWork.ExerciseRepo.Insert(new Exercise
                {
                    TaskName = ".Net Task 1",
                    TaskTextField = "First Task 4 .Net",
                    Course = ".Net",
                    CourseId = unitOfWork.CourseRepo.Get(c => c.Name == ".Net").First().Id,
                    IsDeleted = false,
                    CreateDateTime = System.DateTime.Now,
                    UpdateDateTime = System.DateTime.Now,
                    TeacherId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id,
                    TaskBaseCodeField = "namespace OnlineExam\n{\n\tpublic class Program\n    \t{\n    \t\tpublic static int Test()\n    \t\t\t{\n    \t\t\t\treturn 123;\n                }\n        }\n}"
                });
            }
            if (!context.Exercises.Any(r => r.TaskName == "Java Task 1"))
            {
                unitOfWork.ExerciseRepo.Insert(new Exercise
                {
                    TaskName = "Java Task 1",
                    TaskTextField = "First Task 4 Java",
                    Course = "Java",
                    CourseId = unitOfWork.CourseRepo.Get(c => c.Name == "Java").First().Id,
                    IsDeleted = false,
                    CreateDateTime = System.DateTime.Now,
                    UpdateDateTime = System.DateTime.Now,
                    TeacherId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
            }
            if (!context.Exercises.Any(r => r.TaskName == "JavaScript Task 1"))
            {
                unitOfWork.ExerciseRepo.Insert(new Exercise
                {
                    TaskName = "JavaScript Task 1",
                    TaskTextField = "First Task 4 JavaScript",
                    Course = "JavaScript",
                    CourseId = unitOfWork.CourseRepo.Get(c => c.Name == "JavaScript").First().Id,
                    IsDeleted = false,
                    CreateDateTime = System.DateTime.Now,
                    UpdateDateTime = System.DateTime.Now,
                    TeacherId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
            }
            
            unitOfWork.Save();
        }
    }
}
