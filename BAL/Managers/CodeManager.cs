using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BAL.Interfaces;
using DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Model.DB;
using Model.DB.Code;
using Model.DTO.CodeDTO;
using Model.Entity;

namespace BAL.Managers
{


    public class CodeManager : ICodeManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private IExerciseManager exerciseManager;
        private UserManager<User> userManager;
        private ISandboxManager sandboxManager;
        public CodeManager(IUnitOfWork unitOfWork, IMapper mapper, IExerciseManager exerciseManager, UserManager<User> userManager, ISandboxManager sandboxManager)
        {
            this.sandboxManager = sandboxManager;
            this.userManager = userManager;
            this.exerciseManager = exerciseManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public UserCodeDTO GetUserCodeById(string id)
        {
            UserCodeDTO code = mapper.Map<UserCodeDTO>(unitOfWork.CodeRepo.Get(c => c.UserId == id).FirstOrDefault());
            return code;
        }

        public UserCodeDTO UserCodeByExId(string userId, int exerciseId)
        {
            UserCodeDTO code = mapper.Map<UserCodeDTO>(unitOfWork.CodeRepo.Get(c => c.UserId == userId && c.ExerciseId == exerciseId).FirstOrDefault());
            return code;
        }

        public string IsUserDidExercise(string userId, int exerciseId)
        {
            return unitOfWork.CodeRepo.Get(c => c.UserId == userId && c.ExerciseId == exerciseId).LastOrDefault()?.CodeText;
        }

        public bool FindUserCode(string userId, int exerciseId)
        {
            return unitOfWork.CodeRepo.Get(c => c.ExerciseId == exerciseId && c.UserId == userId).FirstOrDefault() != null;
        }


        public void AddHistory(int codeId, string text,DateTime date,  string error = null, string result = null)
        {
            unitOfWork.CodeHistoryRepo.Insert(new CodeHistory
            {
                CodeText = text,
                UserCodeId = codeId,
                Error = error,
                Result = result,
                time = date
            });
            unitOfWork.Save();

        }

        public string ExecuteCode(UserCodeDTO model)
        {
            if (FindUserCode(model.UserId, model.ExerciseId))
            {
                var code = unitOfWork.CodeRepo.Get(c => c.ExerciseId == model.ExerciseId && c.UserId == model.UserId).FirstOrDefault();
                if (code != null)
                {
                    code.CodeText = model.CodeText;
                    unitOfWork.CodeRepo.Update(code);
                }
            }
            else
            {
                UserCode code = new UserCode
                {
                    CodeText = model.CodeText,
                    ExerciseId = model.ExerciseId,
                    UserId = unitOfWork.UserRepo.Get().Where(e=>e.UserName== model.UserId).FirstOrDefault().Id
                };

                unitOfWork.CodeRepo.Insert(code);
            }
            unitOfWork.Save();
            return ExecutionResult(model.CodeText, model.ExerciseId, unitOfWork.UserRepo.Get().Where(e => e.UserName == model.UserId).FirstOrDefault().Id);
        }

        public string ExecutionResult(string code, int exId, string userId)
        {
            var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First().Id;
            var res = sandboxManager.Execute(code);
            if (res.Success)
            {
                string result =
                    $"Result: {res.Result};\r\nCompile time: {res.CompileTime.TotalMilliseconds};\r\nExecution Time: {res.ExecutionTime.TotalMilliseconds};";
                AddHistory(codeId, code, DateTime.Now, null, result);
                return result;
            }
            
            string errors = res.CompileTimeExceptions.Aggregate("", (current, v) => current + (v + ";\r\n"));
            errors = res.RunTimeExceptions.Aggregate(errors, (current, v) => current + (v + ";\r\n"));
            AddHistory(codeId, code, DateTime.Now,  errors, null );
            return errors;
        }


        public string GetCode(UserCodeDTO model)
        {
   
                UserCode code = new UserCode
                {
             CodeText = model.CodeText,
                };


                  return ExecuteCode(model.CodeText);
        }

        public string ExecuteCode(string code)
        {
         //   var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First().Id;
            var res = sandboxManager.Execute(code);
            if (res.Success)
            {
                string result =
                    $"Result: {res.Result};\r\nCompile time: {res.CompileTime.TotalMilliseconds};\r\nExecution Time: {res.ExecutionTime.TotalMilliseconds};";
                return result;
            }

            string errors = res.CompileTimeExceptions.Aggregate("", (current, v) => current + (v + ";\r\n"));
            errors = res.RunTimeExceptions.Aggregate(errors, (current, v) => current + (v + ";\r\n"));
            return errors;
        }









        public List<CodeHistory> GetHistoryLst(int codeId)
        {
            var codeHistories = unitOfWork.CodeHistoryRepo.Get().Where(e => e.UserCodeId == codeId).ToList();
            return codeHistories;
        }
        


        public SetFav SetFavouriteCode(SetFav model)
        {
            var codeHistoryEntity = unitOfWork.CodeHistoryRepo.Get().Where(e => e.Id==model.codeId).FirstOrDefault();
            codeHistoryEntity.IsFavouriteCode = !model.flag;
            model.flag = !model.flag;
            unitOfWork.Save();
            return model;
        }
        public CodeModel EditCode(CodeModel codeModel)
        {
            var code = unitOfWork.CodeHistoryRepo.Get().Where(e => e.Id == codeModel.codeTextId).FirstOrDefault();
            code.CodeText = codeModel.codeText;
            unitOfWork.Save();
            return codeModel;
        }


    }
}