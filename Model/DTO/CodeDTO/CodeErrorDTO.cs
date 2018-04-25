using Model.DB;
using Model.DB.Code;

namespace Model.DTO.CodeDTO
{
    public class CodeErrorDTO
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string Result { get; set; }

        public UserCodeDTO Code { get; set; }
    }
}
