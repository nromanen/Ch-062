using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.CodeDTO
{
    public class CodeHistoryDTO
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string CodeText { get; set; }

        public virtual UserCodeDTO Code { get; set; }
    }
}
