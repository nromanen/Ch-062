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

         List<CodeResultDTO> GetCodeResults(int codeId);
         List<CodeErrorDTO> GetCodeErrors(int codeId);

         UserCodeDTO UserCodeByExId(string userId, int exerciseId);

         string IsUserDidExercise(string userId, int exerciseId);

         void AddCode(UserCodeDTO model);

        UserCodeDTO BuildCodeModel(UserCodeDTO model);
    }
}
