//using AutoMapper;
//using BAL.IoC;
//using BAL.Managers;
//using DAL.Interface;
//using Model.DB;
//using NSubstitute;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace BALTest
//{
//    [TestClass]
//    class CourseManagerTest : TestStartup
//    {


//        [TestInitialize]
//        public void Setup2()
//        {
            

//            var sNewsRepo = Substitute.For<IBaseRepository<Course>>();
//            sUoW = Substitute.For<IUnitOfWork>();
//        }

      

//        [TestMethod]
//        public void GetAllTest()
//        {
//            var courseManager = new CourseManager(sUoW, mapper);
//            var result = courseManager.GetAll();
//        }
//    }
//}
