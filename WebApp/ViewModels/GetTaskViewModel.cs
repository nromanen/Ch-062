using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class GetTaskViewModel
    {
        public int ID { get; set; }

        public string TaskName { get; set; }

        public string TaskString { get; set; }

        public short AccessCondition { get; set; }
    }
}
