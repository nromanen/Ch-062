using DAL.Interface;
using BAL.Interfaces;
using System.Collections.Generic;
using Model.DTO;
using Model.DB;
using AutoMapper;


namespace BAL.Managers
{
    public class TestCaseManager : BaseManager, ITestCaseManager
    {
        public TestCaseManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        { }

        public IEnumerable<TestCaseDTO> GetAll()
        {
            return mapper.Map<List<TestCaseDTO>>(unitOfWork.TestCasesRepo.GetAll());
        }

        public TestCaseDTO GetById(int id)
        {
            return mapper.Map<TestCaseDTO>(unitOfWork.TestCasesRepo.GetById(id));
        }

        public IEnumerable<TestCaseDTO> GetByTeacherId(string id)
        {
            return mapper.Map<List<TestCaseDTO>>(unitOfWork.TestCasesRepo.Get(x => x.UserId == id));
        }

        public IEnumerable<TestCaseDTO> GetByExerciseId(int id)
        {
            return mapper.Map<List<TestCaseDTO>>(unitOfWork.TestCasesRepo.Get(x => x.ExerciseId == id));
        }

        public void Insert(TestCaseDTO item)
        {
            unitOfWork.TestCasesRepo.Insert(mapper.Map<TestCase>(item));
            unitOfWork.Save();
        }

        public void Update(TestCaseDTO item)
        {
            unitOfWork.TestCasesRepo.Update(mapper.Map<TestCase>(item));
            unitOfWork.Save();
        }

        public void UpdateTeacher(int testCaseId, string teacherId)
        {
            var testCase = unitOfWork.TestCasesRepo.GetById(testCaseId);
            if (testCase != null)
            {
                testCase.UserId = teacherId;
                unitOfWork.TestCasesRepo.Update(testCase);
                unitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            var testCase = unitOfWork.TestCasesRepo.GetById(id);
            if (testCase != null)
            {
                unitOfWork.TestCasesRepo.Delete(testCase);
                unitOfWork.Save();
            }
        }
    }
}
