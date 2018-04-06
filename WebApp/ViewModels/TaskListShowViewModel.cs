using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class TaskListShowViewModel
    {
        public IEnumerable<TestTask> testTasks { get; set; }
        public TaskListViewModel TaskViewModel { get; set; }
    }
}
