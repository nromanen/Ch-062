using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface ICourseManager
    {
        IEnumerable<CourseDTO> GetAll();
        CourseDTO GetById(int id);
        IEnumerable<CourseDTO> Get(
            Expression<Func<Course, bool>> filter = null,
            Func<IQueryable<Course>,
            IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = "");
        void Insert(CourseDTO item);
        void Update(CourseDTO item);
        void UpdateCourseOwner(int courseId, string teacherId);
        void ToggleCourseStatus(int id);
        void Delete(CourseDTO item);
    }
}
