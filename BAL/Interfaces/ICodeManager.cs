using System;
using System.Collections.Generic;
using System.Text;
using Model.DB.Code;
using Model.DTO.CodeDTO;

namespace BAL.Interfaces
{
    public interface ICodeManager
    {
        UserCodeDTO GetUserCodeById(string id);
        UserCodeDTO UserCodeByExId(string userId, int exerciseId);
        string IsUserDidExercise(string userId, int exerciseId);
        bool FindUserCode(string userId, int exerciseId);
        void AddHistory(int codeId, string text, string error = null, string result = null);
        string ExecuteCode(UserCodeDTO model);
        string ExecutionResult(string code, int exId, string userId);
        UserCodeDTO BuildCodeModel(UserCodeDTO model);
        List<CodeHistory> GetHistoryLst(int codeId);
        void SetFavouriteCode(int codeId, bool setToFavourite);
    }
}
