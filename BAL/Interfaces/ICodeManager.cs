using System;
using System.Collections.Generic;
using System.Text;
using Model.DTO.CodeDTO;

namespace BAL.Interfaces
{
    public interface ICodeManager
    {
        UserCodeDTO GetUserCodeById(string id);
        UserCodeDTO UserCodeByExId(string userId, int exerciseId);
        string IsUserDidExercise(string userId, int exerciseId);
        bool FindUserCode(string userId, int exerciseId);
        string ExecuteCode(UserCodeDTO model);
        string ExecutionResult(string code, int exId, string userId);
        UserCodeDTO BuildCodeModel(UserCodeDTO model);
        IEnumerable<CodeHistoryDTO> GetHistoryLst(int codeId);
    }
}
