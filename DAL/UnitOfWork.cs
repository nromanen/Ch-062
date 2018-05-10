using System;
using DAL.Repositories;
using Model.DB;
using DAL.Interface;
using DAL.Migrations;
using Microsoft.AspNetCore.Identity;
using Model.DB.Code;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext context;

        private IBaseRepository<IdentityRole> roleRepo;
        private IBaseRepository<User> userRepo;
        private IBaseRepository<Exercise> exerciseRepo;
        private IBaseRepository<Course> courseRepo;
        private IBaseRepository<UserCode> codeRepo;
        private IBaseRepository<CodeHistory> codeHistoryRepo;
        private IBaseRepository<TestCase> testCasesRepo;
        private IBaseRepository<Comment> commentRepo;
        private IBaseRepository<News> newsRepo;

        public UnitOfWork(MainDbContext context)
        {
            this.context = context;
        }

        public IBaseRepository<IdentityRole> RoleRepo
        {
            get
            {
                if (roleRepo == null) { roleRepo = new BaseRepository<IdentityRole>(context); }
                return roleRepo;
            }
        }

        public IBaseRepository<User> UserRepo
        {
            get
            {
                if (userRepo == null) { userRepo = new BaseRepository<User>(context); }
                return userRepo;
            }
        }

        public IBaseRepository<Exercise> ExerciseRepo
        {
            get
            {
                if (exerciseRepo == null) { exerciseRepo = new BaseRepository<Exercise>(context); }
                return exerciseRepo;
            }
        }

        public IBaseRepository<Comment> CommentRepo
        {
            get
            {
                if (commentRepo == null) { commentRepo = new BaseRepository<Comment>(context); }
                return commentRepo;
            }
        }

        public IBaseRepository<Course> CourseRepo
        {
            get
            {
                if (courseRepo == null) { courseRepo = new BaseRepository<Course>(context); }
                return courseRepo;
            }
        }

        public IBaseRepository<UserCode> CodeRepo
        {
            get
            {
                if (codeRepo == null) { codeRepo = new BaseRepository<UserCode>(context); }
                return codeRepo;
            }
        }

       
        public IBaseRepository<CodeHistory> CodeHistoryRepo
        {
            get
            {
                if (codeHistoryRepo == null) { codeHistoryRepo = new BaseRepository<CodeHistory>(context); }
                return codeHistoryRepo;
            }
        }
        
        public IBaseRepository<TestCase> TestCasesRepo
        {
            get
            {
                if (testCasesRepo == null) { testCasesRepo = new BaseRepository<TestCase>(context); }
                return testCasesRepo;
            }
        }

        public IBaseRepository<News> NewsRepo
        {
            get
            {
                if (newsRepo == null) { newsRepo = new BaseRepository<News>(context); }
                return newsRepo;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        private bool isDisposed = false;

        protected virtual void Grind(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Grind(true);
            GC.SuppressFinalize(this);
        }
    }
}
