using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Exercise
    {  
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public string TaskName { get; set; }

        public string TaskTextField { get; set; }

        public string TaskBaseCodeField { get; set; }

        public int CourseId { get; set; }

        public virtual string Course{ get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}
