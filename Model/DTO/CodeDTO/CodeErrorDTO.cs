using Model.DB;

namespace Model.DTO.CodeDTO
{
    public class CodeErrorDTO
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string Result { get; set; }

        public UserCode Code { get; set; }
    }
}
