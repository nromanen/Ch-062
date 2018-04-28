using System.Collections.Generic;
using Model.DTO.CodeDTO;

namespace BAL.Interfaces
{
    public interface ICodeManager
    {
        void AddCode(UserCodeDTO model);
        UserCodeDTO BuildCodeModel(UserCodeDTO model);
        bool FindUserCode(string userId, int exerciseId);
        List<CodeErrorDTO> GetCodeErrors(int codeId);
        List<CodeResultDTO> GetCodeResults(int codeId);
        UserCodeDTO GetUserCodeById(string id);
        string IsUserDidExercise(string userId, int exerciseId);
        UserCodeDTO UserCodeByExId(string userId, int exerciseId);
    }
}