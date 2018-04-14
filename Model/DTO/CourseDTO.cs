using System;
using Model.DB;

namespace Model.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }    
    }
}
