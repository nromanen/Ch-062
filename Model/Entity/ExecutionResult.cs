using System;
using System.Collections.Generic;

namespace Model.Entity
{
    public class ExecutionResult
    {
        public bool Success { get; set; }
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
