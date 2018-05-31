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
using Model.DTO;
using Microsoft.AspNetCore.Identity;

namespace BALTest
{
    [TestClass]
   public class ExerciseManagmentTests : TestStartup
    {


        [TestInitialize]
        public void Setup1()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            mapper = config.CreateMapper();
            sUoW = Substitute.For<IUnitOfWork>();

            var sExerciseRepo = Substitute.For<IBaseRepository<Exercise>>();
 
            var moqExercise = new List<Exercise>() { new Exercise() {TaskName = "TestTask",CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now,TaskTextField ="This is UnitTest task" } };
            sExerciseRepo.GetAll().Returns(moqExercise);

        }
        [TestMethod]
        public void ExerciseTestMethodGetAll()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.GetAll().Returns(new List<Exercise>() { new Exercise() { TaskName = "TestTask", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is UnitTest task" } });
            Assert.AreEqual("TestTask", exerciseManager.GetAll().FirstOrDefault().TaskName);
        }

        [TestMethod]
        public void ExerciseTestMethodInsert()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            exerciseManager.Insert(new ExerciseDTO() { Id = 1, TaskName = "TestTask3", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" });
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();
        }

        [TestMethod]
        public void ExerciseTestMethodGet()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.Insert(new Exercise(){ TaskName = "TaskName", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" });
            sUoW.ExerciseRepo.Get(c => c.TaskName == "TaskName").Returns(new List<Exercise>() { new Exercise() { TaskName = "TaskName", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" } });
            var exercise = exerciseManager.Get(c => c.TaskName == "TaskName").FirstOrDefault();
            Assert.AreEqual("TestTask",exercise.TaskName);
        }
        
        [TestMethod]
        public void ExerciseTestMethodDelete()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            var result = exerciseManager.Get(c => c.TaskName == "TestTask").FirstOrDefault();
            sUoW.ExerciseRepo.GetById(1).Returns(new Exercise { Id = 1, TaskName = "TestTask3", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" });
            exerciseManager.Delete(new ExerciseDTO { Id = 1, TaskName = "TestTask3", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is third UnitTest task" });
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();
        }

        [TestMethod]
        public void ExerciseTestMethodGetById()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.GetById(18).Returns(new Exercise { Id = 18, TaskName = "TestTask4", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            Assert.AreEqual("TestTask4", exerciseManager.GetById(18).TaskName);
        }

        [TestMethod]
        public void ExerciseTestMethodUpdate()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.GetById(18).Returns(new Exercise { Id = 18, TaskName = "TestTask5", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            exerciseManager.Update(18, "TaskTest5","TaskText for fifth task","//code",1,".Net",DateTime.Now,"//testcode");
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();
           // Assert.AreEqual("TestTask5", exerciseManager.GetById(18).TaskName);
        }

        [TestMethod]
        public void ExerciseTestMethodDeleteOrRecover()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.GetById(19).Returns(new Exercise { Id = 19, TaskName = "TestTask5", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            exerciseManager.DeleteOrRecover(19);
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();
        }

        [TestMethod]
        public void ExerciseTestMethodUpdateRating()
        {
            var exerciseManager = new ExerciseManager(sUoW, mapper);
            sUoW.ExerciseRepo.GetById(18).Returns(new Exercise { Id = 18, TaskName = "TestTask5", CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now, TaskTextField = "This is fourth UnitTest task" });
            exerciseManager.UpdateRating(18, 5);
            sUoW.Received(1).Save();
            sUoW.ClearReceivedCalls();           
        }
    }
}
