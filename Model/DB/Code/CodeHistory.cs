using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB.Code
{
    public class CodeHistory
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string CodeText { get; set; }
        
        public virtual UserCode Code { get; set; }
    }
}
