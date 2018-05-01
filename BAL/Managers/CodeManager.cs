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


        public void AddHistory(int codeId, string text, string error = null, string result = null)
        {
            unitOfWork.CodeHistoryRepo.Insert(new CodeHistory
            {
                CodeText = text,
                UserCodeId = codeId,
                Error = error,
                Result = result
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
                    UserId = model.UserId,
                    ExerciseId = model.ExerciseId
                };

                unitOfWork.CodeRepo.Insert(code);
            }
            unitOfWork.Save();
            return ExecutionResult(model.CodeText, model.ExerciseId, model.UserId);
        }

        public string ExecutionResult(string code, int exId, string userId)
        {
            var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First().Id;
            var userCode = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First();
            var res = sandboxManager.Execute(code);
            if (res.Success)
            {
                string result =
                    $"Result: {res.Result}, Compile time: {res.CompileTime.TotalMilliseconds}, Execution Time: {res.ExecutionTime.TotalMilliseconds}";
                AddHistory(codeId, code, null, result);
                return result;
            }
            string errors = res.CompileTimeExceptions.Aggregate("", (current, v) => current + (" " + v));
            errors = res.RunTimeExceptions.Aggregate(errors, (current, v) => current + (" " + v));
            AddHistory(codeId, code, errors, null );
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

        public List<CodeHistory> GetHistoryLst(int codeId)
        {
            var codeHistories = unitOfWork.CodeHistoryRepo.Get().Where(e => e.UserCodeId == codeId).ToList();
            return codeHistories;
        }
        


        public void SetFavouriteCode(int codeId, bool setToFavourite)
        {
            var codeHistoryEntity = unitOfWork.CodeHistoryRepo.Get().Where(e => e.UserCodeId == codeId).FirstOrDefault();
            codeHistoryEntity.IsFavouriteCode = setToFavourite;
            unitOfWork.Save();
        }

    }
}