using Model.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateTestCase
    {
        public IEnumerable<ExerciseDTO> ExerciseList { get; set; }
        public TestCaseDTO TestCaseDTO { get; set; }
        [Required]
        public int ExerciseId { get; set; }
    }
}
