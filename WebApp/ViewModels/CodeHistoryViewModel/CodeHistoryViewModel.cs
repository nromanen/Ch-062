using System;
using System.Collections.Generic;
using System.Linq;
using Model.DTO.CodeDTO;
using System.Threading.Tasks;

namespace WebApp.ViewModels.CodeHistoryViewModel
{
    public class CodeHistoryViewModel
    {
        public IEnumerable<CodeErrorDTO> ErrorExecutionHistory { get; set; }
        public IEnumerable<CodeResultDTO> SuccedExecutionHistory { get; set; }
    }
}
