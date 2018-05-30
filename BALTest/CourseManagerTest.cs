using AutoMapper;
using BAL.IoC;
using BAL.Managers;
using DAL.Interface;
using Model.DB;
using NSubstitute;
using NUnit.Framework;

namespace BALTest
{
    [TestFixture]
    class CourseManagerTest : TestStartup
    {
        IMapper mapper;
        IUnitOfWork sUoW;

        [SetUp]
        public override void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            mapper = config.CreateMapper();

            var sNewsRepo = Substitute.For<IBaseRepository<Course>>();
            sUoW = Substitute.For<IUnitOfWork>();
        }

        static object[] TestCasesForGetAll =
        {
            new object[]{}
        };


        [Test, TestCaseSource("TestCasesForGetAll")]
        public void GetAllTest()
        {
            var courseManager = new CourseManager(sUoW, mapper);
            var result = courseManager.GetAll();
        }
    }
}
