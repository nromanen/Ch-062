using System;


namespace Model.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }

        public string UserDTOId { get; set; }
        public virtual UserDTO UserDTO { get; set; }
    }
}
