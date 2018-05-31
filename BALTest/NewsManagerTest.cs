using AutoMapper;
using BAL.IoC;
using BAL.Managers;
using DAL.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.DB;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace BALTest
{
    [TestClass]
    public class NewsManagerTest
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

            var sNewsRepo = Substitute.For<IBaseRepository<News>>();
            sUoW = Substitute.For<IUnitOfWork>();

            var moqNews = new List<News>() { new News() { Text = "Hello World" } };
            sNewsRepo.GetAll().Returns(moqNews);
            sUoW.MessageRepo.Returns(sNewsRepo);
        }
        [TestMethod]
        public void TestMethod1()
        {
            var newsManager = new NewsManager(sUoW, mapper);
            var result = newsManager.GetAll();
            sUoW.Received(0).Save();
            sUoW.ClearReceivedCalls();

            Assert.AreEqual("Hello World", result.First().Text);
        }
    }
}
