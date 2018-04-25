using System.Collections.Generic;
using Model.DB;
using Model.DB.Code;

namespace Model.DTO.CodeDTO
{
    public class UserCodeDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string CodeText { get; set; }

        public ICollection<CodeErrorDTO> Errors { get; set; }
        public ICollection<CodeResultDTO> Results { get; set; }

        public UserDTO User { get; set; }
        public ExerciseDTO Exercise { get; set; }
    }
}
