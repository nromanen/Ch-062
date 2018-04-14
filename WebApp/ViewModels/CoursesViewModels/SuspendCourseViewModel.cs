using Model.DTO;
using System.Collections.Generic;

namespace WebApp.ViewModels.CoursesViewModels
{
    public class SuspendCourseViewModel
    {
        public IEnumerable<UserDTO> TeacherList { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string ResultTeacherId { get; set; }
    }
}
