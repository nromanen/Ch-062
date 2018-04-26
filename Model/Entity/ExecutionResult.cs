using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public class ExecutionResult
    {
        public string Result { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public TimeSpan CompileTime { get; set; }
        public List<string> CompileTimeExceptions { get; set; }
        public List<string> RunTimeExceptions { get; set; }

        public ExecutionResult()
        {
            ExecutionTime = new TimeSpan();
            CompileTime = new TimeSpan();
            CompileTimeExceptions = new List<string>();
            RunTimeExceptions = new List<string>();
        }
    }
}
