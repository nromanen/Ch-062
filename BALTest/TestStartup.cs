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

namespace BALTest
{
    [TestClass]
    public class TestStartup
    {

        protected IMapper mapper;
        protected IUnitOfWork sUoW;

        [ClassInitialize]
        public virtual void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            mapper = config.CreateMapper();
            sUoW = Substitute.For<IUnitOfWork>();

           
        }
    }
}
