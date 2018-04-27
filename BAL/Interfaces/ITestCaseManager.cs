using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface ITestCaseManager
    {
        IEnumerable<TestCaseDTO> GetAll();
        TestCaseDTO GetById(int id);
        IEnumerable<TestCaseDTO> GetByTeacherId(string id);
        IEnumerable<TestCaseDTO> GetByExerciseId(int id);
        void Insert(TestCaseDTO item);
        void Update(TestCaseDTO item);
        void UpdateTeacher(int testCaseId, string teacherId);
        void Delete(int id);
    }
}
