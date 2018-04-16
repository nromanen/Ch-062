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
        public string Course { get; set; }

        [Required]
        public string TaskName { get; set; }

        public string TaskString { get; set; }

        public DateTime CreateDateTime { get; set; }

    }
}
