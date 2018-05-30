using System;
using System.Collections.Generic;
using AutoMapper;
using BAL.IoC;
using BAL.Managers;
using DAL.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DB;
using NSubstitute;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BALTest
{
    [TestClass]
   public class ExerciseManagmentTests
    {
        IMapper mapper;
        IUnitOfWork sUoW;

        [TestInitialize]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            mapper = config.CreateMapper();
             
            var sExerciseRepo = Substitute.For<IBaseRepository<Exercise>>();
            sUoW = Substitute.For<IUnitOfWork>();


            var moqExercise = new List<Exercise>() { new Exercise() { TaskName = "TestTask",CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now,TaskTextField ="This is UnitTest task" } };
            sExerciseRepo.GetAll().Returns(moqExercise);



            sUoW.ExerciseRepo.Returns(sExerciseRepo);
        }


        [TestMethod]
        public void ExerciseTestMethod1()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            var result = exerciseManager.GetAll();
           // sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();

            Assert.AreEqual("TestTask", result.First().TaskName);
        }
        [TestMethod]
        public void ExerciseTestMethod2()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            var result = exerciseManager.Get(c=>c.TaskName == "TestTask");
            //sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();
            Assert.AreEqual("TestTask", result.First().TaskName);
        }
    }
}
