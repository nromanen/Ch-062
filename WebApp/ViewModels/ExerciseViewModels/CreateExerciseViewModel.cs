using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.ViewModels
{
    public class CreateExerciseViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public string TaskTextField { get; set; }

        public string TaskBaseCodeField { get; set; }

    }
}
