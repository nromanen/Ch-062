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
   public class ExerciseManagmentTests : TestStartup
    {


        [ClassInitialize]
        public void Setup1()
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new AutoMapperProfileConfiguration());
            //});
            //mapper = config.CreateMapper();
            //sUoW = Substitute.For<IUnitOfWork>();

            var sExerciseRepo = Substitute.For<IBaseRepository<Exercise>>();
 
            var moqExercise = new List<Exercise>() { new Exercise() {TaskName = "TestTask",CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now,TaskTextField ="This is UnitTest task" } };
            sExerciseRepo.GetAll().Returns(moqExercise);


            sUoW.ExerciseRepo.Returns(sExerciseRepo);

        }


        [TestMethod]
        public void ExerciseTestMethodGetAll()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            var result = exerciseManager.GetAll();
           sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();
            Assert.AreEqual("TestTask", result.First().TaskName);
        }
        [TestMethod]
        public void ExerciseTestMethodGet()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            var result = exerciseManager.Get(c=>c.TaskName == "TestTask");
            sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();
           Assert.AreEqual("TestTask", result.First().TaskName);
        }
        [TestMethod]
        public void ExerciseTestMethodInsert()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new Model.DTO.ExerciseDTO { TaskName = "TestTask2", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is second UnitTest task" });
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();

        }
        [TestMethod]
        public void ExerciseTestMethodDelete()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new Model.DTO.ExerciseDTO { TaskName = "TestTask3", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" });
            var result = exerciseManager.Get(c => c.TaskName == "TestTask3").FirstOrDefault();
            exerciseManager.Delete(result);
            sUoW.Received(2).Save();
            sUoW.ClearReceivedCalls();
        }
        [TestMethod]
        public void ExerciseTestMethodGetById()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new Model.DTO.ExerciseDTO { Id = 18, TaskName = "TestTask4", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            var result = exerciseManager.GetById(18);
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();
            Assert.AreEqual("TestTask4", result.TaskName);
        }
        [TestMethod]
        public void ExerciseTestMethodUpdate()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new Model.DTO.ExerciseDTO { Id = 19, TaskName = "TestTask5", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            exerciseManager.Update(19, "TaskTest5","TaskText for fifth task","//code",1,".Net",DateTime.Now,"//testcode");
            sUoW.Received(2).Save();
            sUoW.ClearReceivedCalls();
        }
        [TestMethod]
        public void ExerciseTestMethodDeleteOrRecover()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new Model.DTO.ExerciseDTO { Id = 19, TaskName = "TestTask5", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            exerciseManager.DeleteOrRecover(19);
            sUoW.Received(2).Save();
            sUoW.ClearReceivedCalls();
        }
    }
}
