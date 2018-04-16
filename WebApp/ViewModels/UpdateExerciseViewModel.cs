using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class UpdateExerciseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        public string TaskString { get; set; }

        [Required]
        public string Course { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}
