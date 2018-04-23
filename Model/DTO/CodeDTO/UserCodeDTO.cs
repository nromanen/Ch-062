using System.Collections.Generic;
using Model.DB;

namespace Model.DTO.CodeDTO
{
    public class UserCodeDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string CodeText { get; set; }

        public ICollection<CodeErrorDTO> Errors { get; set; }
        public ICollection<CodeResult> Results { get; set; }

        public User User { get; set; }
        public Exercise Exercise { get; set; }
    }
}
