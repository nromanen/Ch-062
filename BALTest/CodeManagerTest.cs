using AutoMapper;
using BAL.IoC;
using DAL.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DB.Code;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BAL.Managers;
using BAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Model.DB;

namespace BALTest
{
    [TestClass]
  public  class CodeManagerTest
    {     
           IMapper mapper;
            IUnitOfWork sUoW;

        private readonly UserManager<User> userManager;

        [TestInitialize]
            public void Setup()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfileConfiguration());
                });
                mapper = config.CreateMapper();

                var sCodeRepo = Substitute.For<IBaseRepository<UserCode>>();
                sUoW = Substitute.For<IUnitOfWork>();


                var moqCode = new List<UserCode>() { new UserCode() {Mark=5, CodeText="code",   EndTime=DateTime.Now } };



                  //  var mooq = new List<CodeHistory>() { new CodeHistory() {  UserCode = moqNews} };
            sCodeRepo.GetAll().Returns(moqCode);
            


                sUoW.CodeRepo.Returns(sCodeRepo);
            }


        [TestMethod]
        public void TestMethod2()
        {
            var exma = new ExerciseManager(sUoW, mapper);
            var sandb = new SandboxManager("SandboxAPI");
            var codeManager = new CodeManager(sUoW, mapper, exma, userManager , sandb);
            var result = codeManager.Get(e=>e.Mark == 5);
           // sUoW.Received(0).Save();
          //  sUoW.ClearReceivedCalls();
          
            Assert.AreEqual(5, result.FirstOrDefault().Mark);
  
          //  Assert.AreEqual("Hello World", result.First().Text);
        }

        [TestMethod]
        public void TestMethod3()
        {


            var exma = new ExerciseManager(sUoW, mapper);
            var sandb = new SandboxManager("SandboxAPI");
            var codeManager = new CodeManager(sUoW, mapper, exma, userManager, sandb);
            var result = codeManager.GetAll();
            sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();

            Assert.AreEqual("dadada", result.First().UserCode);

            //  Assert.AreEqual("Hello World", result.First().Text);
        }





    }

}
