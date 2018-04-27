using System.Collections.Generic;
using Model.DTO.CodeDTO;

namespace Model.DB.Code
{
    public class UserCode
    {
        public UserCode()
        {
            this.CodeErrors = new HashSet<CodeError>();
            this.CodeResults = new HashSet<CodeResult>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string CodeText { get; set; }

        public virtual ICollection<CodeError> CodeErrors { get; set; }
        public virtual ICollection<CodeResult> CodeResults { get; set; }

        public virtual User User { get; set; }
        public virtual Exercise Exercise { get; set; }


    }
}
