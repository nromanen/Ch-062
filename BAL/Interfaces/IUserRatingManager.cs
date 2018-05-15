using System;
using System.Collections.Generic;
using System.Text;
using Model.DB;
using Model.DTO;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IUserRatingManager 
    {
        IEnumerable<UserDTO> GetAll();
    }
}
