using System.Linq;
using Microsoft.AspNetCore.Identity;
using DAL.Interface;
using Model.DB;

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

                string userTeacher2 = "teacher2@gmail.com";
                userManager.CreateAsync(new User { UserName = userTeacher2, Email = userTeacher2, EmailConfirmed = true }, passwordTeacher).Wait();
                var t2 = userManager.FindByNameAsync(userTeacher2);
                userManager.AddToRoleAsync(t2.Result, "Teacher").Wait();
            }
            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                roleManager.CreateAsync(new IdentityRole("Student")).Wait();
                string userStudent = "student@gmail.com";
                string passwordStudent = "Student_123";
                userManager.CreateAsync(new User { UserName = userStudent, Email = userStudent, EmailConfirmed = true }, passwordStudent).Wait();
                var t = userManager.FindByNameAsync(userStudent);
                userManager.AddToRoleAsync(t.Result, "Student").Wait();

                string userStudent2 = "student2@gmail.com";
                userManager.CreateAsync(new User { UserName = userStudent2, Email = userStudent2, EmailConfirmed = true }, passwordStudent).Wait();
                var t2 = userManager.FindByNameAsync(userStudent2);
                userManager.AddToRoleAsync(t2.Result, "Student").Wait();

                string userStudent3 = "student3@gmail.com";
                userManager.CreateAsync(new User { UserName = userStudent3, Email = userStudent3, EmailConfirmed = true }, passwordStudent).Wait();
                var t3 = userManager.FindByNameAsync(userStudent3);
                userManager.AddToRoleAsync(t3.Result, "Student").Wait();
            }
            if (!context.Courses.Any(r => r.Name == "C# Starter"))
            {
                unitOfWork.CourseRepo.Insert(new Course
                {
                    Name = "C# Starter",
                    Description = "Courses for .Net starter group",
                    IsActive = true,
                    CreationDate = System.DateTime.Now,
                    UserId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id
                });
            }
            unitOfWork.Save();
            if (!context.Exercises.Any(r => r.TaskName == "Simple addition"))
            {
                string code = 
@"public class Program
{
    public static int Addition(int a, int b)
    {
        return a + b;
    }
}";
                string testCasesCode = 
@"using NUnit.Framework;
[TestFixture]
public class UnitTest
{
    [Test, TestCaseSource(""Cases"")]
    public void TestMethod(int expected, int a, int b)
    {
        Assert.AreEqual(expected, Program.Addition(a, b));
    }
    static object[] Cases =
    {
        new object[] { 12, 8, 4 },
        new object[] { 12, 6, 6 },
        new object[] { 12, 2, 10 }
    };
}";
                unitOfWork.ExerciseRepo.Insert(new Exercise
                {
                    TaskName = "Simple addition",
                    TaskTextField = "First Task 4 .Net",
                    Course = "DotNet Starter",
                    CourseId = unitOfWork.CourseRepo.Get(c => c.Name == "DotNet Starter").First().Id,
                    IsDeleted = false,
                    CreateDateTime = System.DateTime.Now,
                    UpdateDateTime = System.DateTime.Now,
                    TeacherId = unitOfWork.UserRepo.Get(c => c.Email == "teacher@gmail.com").First().Id,
                    TaskBaseCodeField = code,
                    TestCasesCode = testCasesCode
                });
            }
            unitOfWork.Save();
        }
    }
}
