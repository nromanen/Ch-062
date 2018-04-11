using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TaskDTO
    {
        public int ID { get; set; }

        public string TeacherID { get; set; }

        public string TaskName { get; set; }

        public string TaskString { get; set; }

        public string Course { get; set; }
    }
}
