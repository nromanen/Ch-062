using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class CodeResult
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string Result { get; set; }

        public virtual UserCode Code { get; set; }
    }
}
