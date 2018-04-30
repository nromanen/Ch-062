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

        public List<CodeResultDTO> GetCodeResults(int codeId)
        {
            List<CodeResultDTO> results = mapper.Map<List<CodeResultDTO>>(unitOfWork.CodeResultsRepo.Get(c => c.CodeId == codeId).ToList());
            return results;
        }
        public List<CodeErrorDTO> GetCodeErrors(int codeId)
        {
            List<CodeErrorDTO> results = mapper.Map<List<CodeErrorDTO>>(unitOfWork.CodeErrorsRepo.Get(c => c.CodeId == codeId).ToList());
            return results;
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

        private void AddSuccesResult(int codeId, string result)
        {
            unitOfWork.CodeResultsRepo.Insert(new CodeResult
            {
                CodeId = codeId,
                Result = result
            });
            unitOfWork.Save();

        }
        private void AddErrors(int codeId, string result)
        {
            unitOfWork.CodeErrorsRepo.Insert(new CodeError
            {
                CodeId = codeId,
                Result = result
            });
            unitOfWork.Save();

        }
        private void AddHistory(int codeId, string text)
        {
            unitOfWork.CodeHistoryRepo.Insert(new CodeHistory
            {
                CodeText = text,
                CodeId = codeId
            });
            unitOfWork.Save();

        }

        public string ExecuteCode(UserCodeDTO model)
        {
            if (FindUserCode(model.UserId, model.ExerciseId))
            {
                var code = unitOfWork.CodeRepo.Get(c => c.ExerciseId == model.ExerciseId && c.UserId == model.UserId)
                    .FirstOrDefault();
                if (code != null)
                {
                    code.CodeText = model.CodeText;
                    unitOfWork.CodeRepo.Update(code);
                    AddHistory(code.Id, model.CodeText);
                }
            }
            else
            {
                UserCode code = new UserCode
                {
                    CodeText = model.CodeText,
                    UserId = model.UserId,
                    ExerciseId = model.ExerciseId
                };

                unitOfWork.CodeRepo.Insert(code);
            }
            unitOfWork.Save();
            return ExecutionResult(model.CodeText, model.ExerciseId, model.UserId);
        }

        public string ExecutionResult(string code, int exId, string userId )
        {
            var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId)
                .First().Id;
            var res = sandboxManager.Execute(code);
            if (res.Success)
            {
                string result =
                    $"Result: {res.Result}, Compile time: {res.CompileTime.TotalMilliseconds}, Execution Time: {res.ExecutionTime.TotalMilliseconds}";
                AddSuccesResult(codeId,result);
                return result;
            }
            string errors = res.CompileTimeExceptions.Aggregate("", (current, v) => current + (" " + v));
            errors = res.RunTimeExceptions.Aggregate(errors, (current, v) => current + (" " + v));
            AddErrors(codeId,errors);
            return errors;
        }
        public UserCodeDTO BuildCodeModel(UserCodeDTO model)
        {
            var exercise = exerciseManager.GetById(model.ExerciseId);
            model.Exercise = exercise;
            model.CodeText = exercise.TaskBaseCodeField;
            var user = userManager.FindByNameAsync(model.UserId).Result;
            model.UserId = user.Id;
            string text = IsUserDidExercise(user.Id, exercise.Id);
            if (text != null)
            {
                model.CodeText = text;
            }
            return model;
        }
    }
}