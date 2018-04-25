using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using Model.DB;
using Model.DB.Code;

namespace BAL.Managers
{
    public class CodeManager
    {
        private IUnitOfWork unitOfWork;
        public CodeManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserCode GetUserCodeById(string id)
        {
            UserCode code = unitOfWork.CodeRepo.GetAll().FirstOrDefault(c => c.UserId == id);
            return code;
        }

        public List<CodeResult> GetCodeResults(int codeId)
        {
            List<CodeResult> results = unitOfWork.CodeResultsRepo.GetAll().ToList().Where(c => c.CodeId == codeId).ToList();
            return results;
        }
    }
}