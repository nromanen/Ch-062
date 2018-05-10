using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual IEnumerable<UserCodeDTO> Get(Expression<Func<UserCode, bool>> filter = null,
                             Func<IQueryable<UserCode>,
                             IOrderedQueryable<UserCode>> orderBy = null,
                             string includeProperties = "")
        {
            return mapper.Map<List<UserCodeDTO>>(unitOfWork.CodeRepo.Get(filter, orderBy, includeProperties));
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

        public List<UserCodeDTO> UserCodeListByExId(int exerciseId)
        {
            var code = mapper.Map<List<UserCodeDTO>>(unitOfWork.CodeRepo.Get(c => c.ExerciseId == exerciseId));
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
            var status = model.CodeStatus;
            if (FindUserCode(model.UserId, model.ExerciseId))
            {
                var code = unitOfWork.CodeRepo.Get(c => c.ExerciseId == model.ExerciseId && c.UserId == model.UserId).FirstOrDefault();
                if (code != null)
                {
                    status = code.CodeStatus;
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
            return ExecutionResult(model.CodeText, model.ExerciseId, model.UserId, status);
        }

        public string ExecutionResult(string code, int exId, string userId, CodeStatus codeStatus)
        {
            var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First().Id;
            var res = sandboxManager.Execute(code);
            if (res.Success)
            {
                string result =
                    $"Result: {res.Result};\r\nCompile time: {res.CompileTime.TotalMilliseconds};\r\nExecution Time: {res.ExecutionTime.TotalMilliseconds};";
                if(codeStatus != CodeStatus.Done)
                {
                    AddHistory(codeId, code, DateTime.Now, null, result);
                }
                return result;
            }
            
            string errors = res.CompileTimeExceptions.Aggregate("", (current, v) => current + (v + ";\r\n"));
            errors = res.RunTimeExceptions.Aggregate(errors, (current, v) => current + (v + ";\r\n"));
            if(codeStatus != CodeStatus.Done)
            {
                AddHistory(codeId, code, DateTime.Now, errors, null);
            }
            return errors;
        }



        public string GetCode(UserCodeDTO model)
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
            return GetResult(model.CodeText, model.ExerciseId, model.UserId);
        }


        public string GetResult(string code, int exId, string userId)
        {
            var codeId = unitOfWork.CodeRepo.Get(c => c.ExerciseId == exId && c.UserId == userId).First().Id;

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


        public void SetCodeStatus(int id)
        {
            var code = unitOfWork.CodeRepo.GetById(id);
            code.CodeStatus = CodeStatus.Done;
            code.EndTime = DateTime.Now;
            unitOfWork.CodeRepo.Update(code);
            unitOfWork.Save();
        }

        public void SetMark(int id, int mark, string comment)
        {
            var code = unitOfWork.CodeRepo.GetById(id);
            code.Mark = mark;
            code.TeachersComment = comment;
            unitOfWork.CodeRepo.Update(code);
            unitOfWork.Save();
        }

        public UserCodeDTO BuildCodeModel(UserCodeDTO model)
        {
            var exercise = exerciseManager.GetById(model.ExerciseId);
            model.Exercise = exercise;
            model.CodeText = exercise.TaskBaseCodeField;
            var user = userManager.FindByNameAsync(model.UserId).Result;
            model.UserId = user.Id;
            var code = unitOfWork.CodeRepo.Get(c=>c.ExerciseId==model.ExerciseId && c.UserId==model.UserId).FirstOrDefault();
            if (code != null)
            {
                model.CodeText = code.CodeText;
                model.CodeStatus = code.CodeStatus;
                model.Mark = code.Mark;
                model.TeachersComment = code.TeachersComment;
            }
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
        


        public SetFav SetFavouriteCode(SetFav model)
        {
                var codeHistoryEntity = unitOfWork.CodeHistoryRepo.Get().Where(e => e.UserCodeId == model.codeId).FirstOrDefault();
            codeHistoryEntity.IsFavouriteCode = model.flag;
            unitOfWork.Save();
            return model;
        }
        public void EditCode(int codeTextId)
        {

        }


    }
}