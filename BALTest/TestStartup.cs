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
    public class TestStartup
    {

        IMapper mapper;
        IUnitOfWork sUoW;

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
