using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class TestTask
    {
        public int ID { get; set; }

        public int TeacherID { get; set; }

        public string TaskString { get; set; }

        public short AccessCondition { get; set; }
    }
}
