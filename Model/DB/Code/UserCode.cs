using System.Collections.Generic;
using Model.DTO.CodeDTO;

namespace Model.DB.Code
{
    public class UserCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string CodeText { get; set; }

        public virtual ICollection<CodeErrorDTO> Errors { get; set; }
        public virtual ICollection<CodeResult> Results { get; set; }

        public virtual User User { get; set; }
        public virtual Exercise Exercise { get; set; }


    }
}
