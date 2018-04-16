using System;
using System.Collections.Generic;
using System.Text;
using Model.DB;

namespace Model.DTO
{
    public class ExerciseDTO
    {
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public string TaskName { get; set; }

        public string TaskString { get; set; }

        public string Course { get; set; }

        public bool IsDeleted { get; set; }
    }
}
