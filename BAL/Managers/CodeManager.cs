using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Interface;
using Model.DB;
using Model.DB.Code;
using Model.DTO.CodeDTO;

namespace BAL.Managers
{
    public class CodeManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        public CodeManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public UserCodeDTO GetUserCodeById(string id)
        {
            UserCodeDTO code = mapper.Map<UserCodeDTO>(unitOfWork.CodeRepo.GetAll().FirstOrDefault(c => c.UserId == id));
            return code;
        }

        public List<CodeResultDTO> GetCodeResults(int codeId)
        {
            List<CodeResultDTO> results = mapper.Map<List<CodeResultDTO>>(unitOfWork.CodeResultsRepo.GetAll().ToList().Where(c => c.CodeId == codeId).ToList());
            return results;
        }
        public List<CodeErrorDTO> GetCodeErrors(int codeId)
        {
            List<CodeErrorDTO> results = mapper.Map<List<CodeErrorDTO>>(unitOfWork.CodeErrorsRepo.GetAll().ToList().Where(c => c.CodeId == codeId).ToList());
            return results;
        }

        public UserCodeDTO UserCodeByExId(string userId, int exerciseId)
        {
            UserCodeDTO code = mapper.Map<UserCodeDTO>(unitOfWork.CodeRepo.GetAll().ToList().Find(c => c.UserId == userId && c.ExerciseId == exerciseId));
            return code;
        }
    }
}