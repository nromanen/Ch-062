﻿using Model.DB;
using System;
using Microsoft.AspNetCore.Identity;
using Model.DB.Code;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<IdentityRole> RoleRepo { get; }
        IBaseRepository<Exercise> ExerciseRepo { get; }
        IBaseRepository<Course> CourseRepo { get; }
        IBaseRepository<UserCode> CodeRepo { get; }
        IBaseRepository<CodeHistory> CodeHistoryRepo { get; }
        IBaseRepository<TestCase> TestCasesRepo { get; }
        IBaseRepository<Comment> CommentRepo { get; }
        IBaseRepository<News> NewsRepo { get; }
        int Save();
    }
}
