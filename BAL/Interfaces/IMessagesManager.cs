using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DB;
using Model.DTO;

namespace BAL.Interfaces
{
    public interface IMessagesManager
    {
        void Delete(MessagesDTO entityToDelete);
        void DeleteOrRecover(int id);
        IEnumerable<MessagesDTO> Get(Expression<Func<Messages, bool>> filter = null, Func<IQueryable<Messages>, IOrderedQueryable<Messages>> orderBy = null, string includeProperties = "");
        IEnumerable<MessagesDTO> GetAll();
        MessagesDTO GetById(int id);
        void Insert(MessagesDTO entity);
        void Update(MessagesDTO entity);
    }
}