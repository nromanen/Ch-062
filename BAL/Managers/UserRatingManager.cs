using System;
using DAL.Interface;
using BAL.Interfaces;
using System.Collections.Generic;
using Model.DTO;
using System.Linq.Expressions;
using System.Linq;
using Model.DB;
using AutoMapper;


namespace BAL.Managers
{
    public class UserRatingManager : BaseManager, IUserRatingManager
    {
        public UserRatingManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<UserDTO> GetAll()
        {
            return mapper.Map<List<UserDTO>>(unitOfWork.UserRepo.GetAll());
        }
    }
}
