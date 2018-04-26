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
    public class CodeManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private IExerciseManager exerciseManager;
        private UserManager<User> userManager;
        public CodeManager(IUnitOfWork unitOfWork, IMapper mapper, IExerciseManager exerciseManager, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.exerciseManager = exerciseManager;
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

        public string IsUserDidExercise(string userId, int exerciseId)
        {
            return unitOfWork.CodeRepo.Get(c => c.UserId == userId && c.ExerciseId == exerciseId).Last().CodeText;
        }

        public void AddCode(UserCodeDTO model)
        {
            UserCode code = new UserCode
            {
                CodeText = model.CodeText,
                UserId = model.UserId,
                ExerciseId = model.ExerciseId
            };
            unitOfWork.CodeRepo.Insert(code);
            unitOfWork.Save();
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